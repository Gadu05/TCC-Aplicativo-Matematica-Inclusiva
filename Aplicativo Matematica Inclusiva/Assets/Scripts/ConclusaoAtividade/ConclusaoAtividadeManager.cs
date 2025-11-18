using UnityEngine;

public class ConclusaoAtividadeManager : MonoBehaviour
{

    [SerializeField]
    private TMPro.TMP_Text txtMensagem;

    private ExecutadorCiclos executor;

    void Start() {

        executor = FindFirstObjectByType<ExecutadorCiclos>();
        int qtde = executor.getQtdeAtividadesRestantes();

        if (qtde == 0) {
            txtMensagem.text = "Você concluiu todas as atividades! Parabéns!";
        } else {
            txtMensagem.text = $"Você concluiu a atividade! Faltam {qtde} atividades para terminar o ciclo.";
        }
    }


    public void btnProximaAtividade() {
        executor.btnCarregarProximaAtividade();
    }

}
