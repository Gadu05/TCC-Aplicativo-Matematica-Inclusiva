using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;
public class CriarCicloMenuManager : MonoBehaviour {

    [Header("Campos de entrada")]
    [SerializeField] 
    private TMP_InputField inputNome;

    [Header("Referências de UI")] 
    public TMP_InputField campoBusca;
    public Transform contentResultados;
    public Transform contentAtividades;
    public GameObject prefabResultado;
    public GameObject prefabAtividade;
    private AtividadeInfo[] todasAtividades;
    private List<AtividadeInfo> atividadesSelecionadas = new();
    private CicloDAO cicloDAO = new CicloDAO();

    void Start() {
        todasAtividades = Resources.LoadAll<AtividadeInfo>("AtividadesInfo");
        campoBusca.onValueChanged.AddListener(FiltrarAtividades); AtualizarResultados("");
    }

    void FiltrarAtividades(string filtro) { 
        AtualizarResultados(filtro);
    }

    void AtualizarResultados(string filtro) {
        foreach (Transform child in contentResultados) Destroy(child.gameObject);
        // 🔹 Aplica filtro e remove atividades já selecionadas
        var filtradas = todasAtividades.Where(a => a.getNome().ToLower().Contains(filtro.ToLower())).Where(a => !atividadesSelecionadas.Contains(a)).ToList();
        foreach (var atv in filtradas) {
            var item = Instantiate(prefabResultado, contentResultados); item.transform.Find("Nome").GetComponent<TMP_Text>().text = atv.getNome();
            item.transform.Find("AdicionarAtividade").GetComponent<Button>().onClick.AddListener(() => AdicionarAtividade(atv));
        }
    }

    void AdicionarAtividade(AtividadeInfo atividade) {
        if (atividadesSelecionadas.Contains(atividade)) return; atividadesSelecionadas.Add(atividade);
        var item = Instantiate(prefabAtividade, contentAtividades); item.GetComponentInChildren<TMP_Text>().text = $"{atividade.getNome()}";
        //item.AddComponent<DragAndDropItem>();
        var ui = item.AddComponent<AtividadeItemUI>(); 
        ui.setAtividadeInfo(atividade);
        // botão de remover no item
        var btnRemover = item.transform.Find("btnRemover");
        if (btnRemover != null) {
            btnRemover.GetComponent<Button>().onClick.AddListener(() => {
                Destroy(item); atividadesSelecionadas.Remove(atividade);
                AtualizarResultados(campoBusca.text);
                // 🔹 Atualiza a lista de busca
            });
        }
        // 🔹 Atualiza a lista de busca após adicionar
        AtualizarResultados(campoBusca.text);
    }

    public List<AtividadeInfo> ObterAtividadesSelecionadas() {
        return atividadesSelecionadas;
    }

    public void SalvarCiclo() {
        string nome = inputNome.text.Trim();
        if (string.IsNullOrEmpty(nome)) {
            Debug.LogWarning("O nome do ciclo é obrigatório!");
            return;
        }
        List<string> atividadesSelecionadas = new List<string>();
        foreach (Transform child in contentAtividades) {
            var itemUI = child.GetComponent<AtividadeItemUI>();
            if (itemUI != null && itemUI.atividadeInfo != null) 
                atividadesSelecionadas.Add(itemUI.atividadeInfo.getCena());
        }
        if (atividadesSelecionadas.Count == 0) {
            Debug.LogWarning("Selecione pelo menos uma atividade!");
            return;
        }
        try {
            Ciclo ciclo = new Ciclo();
            ciclo.setCodigo(ObjectId.NewObjectId());
            ciclo.setNome(nome);
            ciclo.setAtividades(atividadesSelecionadas);
            foreach (var atv in atividadesSelecionadas) {
                Debug.Log($"Atividade selecionada: {atv}");
            }
            Debug.Log($"Salvando ciclo: {ciclo.nome} com {ciclo.Atividades.Count} atividades.");
            cicloDAO.Inserir(ciclo);
            Debug.Log($"✅ Ciclo '{nome}' salvo com sucesso! ({atividadesSelecionadas.Count} atividades)");
            Debug.Log("Atividades: " + string.Join(", ", atividadesSelecionadas)); SceneManager.LoadScene("Menu");
        } catch (System.Exception ex) {
            Debug.LogError($"Erro ao salvar ciclo: {ex.Message}");
        }
    }
}