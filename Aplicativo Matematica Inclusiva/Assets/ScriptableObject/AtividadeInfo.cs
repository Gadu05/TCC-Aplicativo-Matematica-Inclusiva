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
    private string habilidades; // VERIFICAR POSSIBILIDADE DE USAR ENUM

    [SerializeField]
    private Sprite imagem;  // Imagem representativa da atividade

    public string getNome() { return this.nome; }
    public string getDescricao() { return this.descricao; }
    public string getCena() { return this.cena; }
    public string getHabilidades() { return this.habilidades; }
    public Sprite getImagem() { return this.imagem; }

}