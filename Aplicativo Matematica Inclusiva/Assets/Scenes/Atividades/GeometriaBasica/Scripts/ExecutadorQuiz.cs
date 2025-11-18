using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExecutadorQuiz : MonoBehaviour {

    // Instância única acessível em qualquer cena
    public static ExecutadorQuiz instance { get; private set; }

    public static int pontuacao = 0; // Pontuação atual do jogador
    public static int questaoAtualIndex = 0; // Índice da questão atual
    public int totalQuestoes = 3; // Total de questões no quiz

    private void Awake() {

        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void iniciarQuiz() {
        pontuacao = 0;
        questaoAtualIndex = 0;
        SceneManager.LoadScene("Atividade.GeometriaBasica");
    }

    public int getPontuacao() {
        return pontuacao;
    }

    public int getQuestaoAtualIndex() {
        return questaoAtualIndex;
    }



    public void atualizarPontuacao() {
        pontuacao++;
    }

    public void proximaQuestao() {

        questaoAtualIndex++;

        if (questaoAtualIndex >= totalQuestoes) {
            SceneManager.LoadScene("Atividade.GeometriaBasica.NotaFinal");
            return;
        }

        SceneManager.LoadScene("Atividade.GeometriaBasica");
    }


}
