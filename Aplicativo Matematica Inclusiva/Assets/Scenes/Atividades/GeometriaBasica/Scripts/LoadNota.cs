using UnityEngine;
using UnityEngine.UI;

public class LoadNota : MonoBehaviour {

    Text texto;
    ExecutadorQuiz executador;

    void Start() {
        executador = FindFirstObjectByType<ExecutadorQuiz>();
        texto = GetComponent<Text>();
        texto.text = executador.getPontuacao().ToString() + "/" + executador.totalQuestoes.ToString();
    }

}
