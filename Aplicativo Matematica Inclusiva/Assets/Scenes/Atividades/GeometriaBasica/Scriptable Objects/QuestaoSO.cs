using UnityEngine;

[CreateAssetMenu(fileName = "QuestaoSO", menuName = "Questão")]
public class QuestaoSO : ScriptableObject{

    [SerializeField]
    private Sprite[] imagem;

    [SerializeField]
    private string[] pergunta;

    [SerializeField]
    private string[] respostaCorreta;


    public Sprite[] Imagem { get { return imagem; } }
    public string[] RespostaCorreta { get { return respostaCorreta; } }
    public string[] Pergunta { get { return pergunta; } }

}
