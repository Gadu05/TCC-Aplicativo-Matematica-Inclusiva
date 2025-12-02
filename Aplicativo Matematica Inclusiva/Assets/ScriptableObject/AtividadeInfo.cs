using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NovaAtividade", menuName = "Atividades/Atividade")]
public class AtividadeInfo : ScriptableObject {

    [SerializeField]
    private string nome;          // Nome da atividade
    [SerializeField]
    [TextArea]
    private string descricao;     // Detalhes da atividade
    [SerializeField]
    private string cena;      // Nome da cena a ser carregada

    [SerializeField]
    [TextArea]
    private string resumo;

    [SerializeField]
    [TextArea]
    private string objetivos;

    [SerializeField]
    [TextArea]
    private string habilidades;

    [SerializeField]
    [TextArea]
    private string metodologias;

    [SerializeField]
    private Sprite imagem;  // Imagem representativa da atividade

    public string getNome() { return this.nome; }
    public string getDescricao() { return this.descricao; }
    public string getCena() { return this.cena; }
    public string getResumo() { return this.resumo; }
    public string getObjetivos() { return this.objetivos; }
    public string getHabilidades() { return this.habilidades; }
    public string getMetodologias() { return this.metodologias; }
    public Sprite getImagem() { return this.imagem; }

}