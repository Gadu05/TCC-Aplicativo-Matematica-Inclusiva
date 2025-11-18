using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour {

    public QuestaoSO[] questoes;
    private QuestaoSO questaoAtual;

    /*[Header("Imagens das questões")]
    public Sprite[] ImagemQuestoes; // arraste suas imagens no Inspector
    public string[] respostas;*/

    [Header("UI")]
    public Image imageUI; // componente Image que vai mostrar a questão
    public Text[] respostasUI; // textos dos botões de resposta
    public Text perguntaUI; // texto da pergunta
    public Button next;

    public int questoesDisponiveis; // lista de índices das questões restantes
    public int questaoAtualIndex = -1;
    public int questaoIndex;

    private ExecutadorQuiz executador;

    void Awake() {

        executador = FindFirstObjectByType<ExecutadorQuiz>();
        questaoAtualIndex = executador.getQuestaoAtualIndex();
        Debug.Log("Questão atual index: " + questaoAtualIndex);
        questaoAtual = questoes[questaoAtualIndex];
        GerarRespostasAleatorias();

    }

    void GerarRespostasAleatorias() {

        string[] respostas = questaoAtual.RespostaCorreta;
        Sprite[] imagemQuestoes = questaoAtual.Imagem;

        if (imagemQuestoes.Length == 0) {
            string[] perguntas = questaoAtual.Pergunta;
            questaoIndex = Random.Range(0, perguntas.Length);
            perguntaUI.text = perguntas[questaoIndex];
            imageUI.gameObject.SetActive(false);
        } else {
            questaoIndex = Random.Range(0, imagemQuestoes.Length);
            imageUI.sprite = imagemQuestoes[questaoIndex];
        }
        string respostaCorreta = respostas[questaoIndex];


        // Cria uma lista de respostas (4 opções)
        List<string> opcoes = new List<string>();
        opcoes.Add(respostaCorreta);

        // Adiciona respostas erradas
        while (opcoes.Count < 4) {
            int rnd = Random.Range(0, respostas.Length);
            //string resp = "Resposta " + rnd;
            string resp = respostas[rnd];

            if (!opcoes.Contains(resp)) {
                opcoes.Add(resp);
            }
        }

        // Embaralha as respostas
        for (int i = 0; i < opcoes.Count; i++) {
            int rndIndex = Random.Range(0, opcoes.Count);
            string temp = opcoes[i];
            opcoes[i] = opcoes[rndIndex];
            opcoes[rndIndex] = temp;
        }

        // Mostra nas UI
        for (int i = 0; i < respostasUI.Length; i++) {
            respostasUI[i].text = opcoes[i];
        }
    }

    private int botaoIndex;
    public void BotaoSelecionado(int index) {

        if (!next.IsInteractable()) {
            next.interactable = true;
        }

        botaoIndex = index;

    }

    public void VerificarResposta() {

        string[] respostas = questaoAtual.RespostaCorreta;

        Debug.Log("Resposta selecionada: " + respostasUI[botaoIndex].text);
        Debug.Log("Resposta correta: " + respostas[questaoIndex]);
        Debug.Log("Índice da questão: " + questaoIndex);
        Debug.Log("Índice do botão: " + botaoIndex);
        Debug.Log("BOTAO EXECUTADO");

        if (respostasUI[botaoIndex].text == respostas[questaoIndex]) {
            executador.atualizarPontuacao();
        }

        executador.proximaQuestao();

    }
}
