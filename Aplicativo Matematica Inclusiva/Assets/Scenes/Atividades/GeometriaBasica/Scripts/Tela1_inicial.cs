using UnityEngine;
using UnityEngine.UI;

public class TelaPerguntaSimples : MonoBehaviour
{
    private int idCenario;

    public Text resA;
    public Text resB;
    public Text resC;
    public Text resD;

    public string respostaCorreta; 

    private bool respondido = false;

    void Start()
    {
        idCenario = PlayerPrefs.GetInt("idCenario", 0);
    }

    public void Responder(string respostaSelecionada)
    {
        if (respondido) return; 

        if (respostaSelecionada == respostaCorreta)
        {
            float acertoAtual = PlayerPrefs.GetFloat("acertos" + idCenario.ToString(), 0);
            PlayerPrefs.SetFloat("acertos" + idCenario.ToString(), acertoAtual + 10);
            Debug.Log("Resposta correta!");
        }
        else
        {
            Debug.Log(" Resposta errada");
        }

        respondido = true;
    }
}
