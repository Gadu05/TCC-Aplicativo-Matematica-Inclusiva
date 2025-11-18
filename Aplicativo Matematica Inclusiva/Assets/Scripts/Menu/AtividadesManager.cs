using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AtividadesManager : MonoBehaviour {

    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private Transform contentParent;
    [SerializeField]
    private Sprite notfound;

    [SerializeField]
    private GameObject painelDetalhes;
    [SerializeField]
    private TMP_Text txtObjetivos;
    [SerializeField]
    private TMP_Text txtObjetivos2;
    [SerializeField]
    private TMP_Text txtObjetivos3;

    private AtividadeInfo[] listaAtividades;
    
    void Start() {
        listaAtividades = Resources.LoadAll<AtividadeInfo>("AtividadesInfo");
        LoadAtividades();
    }

    void LoadAtividades() {

        foreach (AtividadeInfo atividade in listaAtividades) {
            
            GameObject obj = Instantiate(itemPrefab, contentParent);

            var button = obj.transform.Find("Card").GetComponent<Button>();
            if (atividade.getImagem() != null) {
                button.transform.Find("Imagem").GetComponent<Image>().sprite = atividade.getImagem();
            } else {
                button.transform.Find("Imagem").GetComponent<Image>().sprite = notfound;
            }

            button.transform.Find("Titulo").GetComponent<TMP_Text>().text = atividade.getNome();
            button.transform.Find("Subtitulo").GetComponent<TMP_Text>().text = atividade.getDescricao();
            button.onClick.AddListener(() => {
                SceneManager.LoadScene(atividade.getCena());
            });

            var detalhes = obj.transform.Find("BotaoDetalhes").GetComponent<Button>();
            detalhes.onClick.AddListener(() => {
                MostrarDetalhes(atividade);
            });


        }

    }
    private void MostrarDetalhes(AtividadeInfo atividade) {
        if (painelDetalhes == null) {
            return;
        }

        txtObjetivos.text = atividade.getDescricao();

        //txtTitulo.text = atividade.getNome();
        //txtDescricao.text = atividade.getDescricao();
        painelDetalhes.SetActive(true);
    }


}