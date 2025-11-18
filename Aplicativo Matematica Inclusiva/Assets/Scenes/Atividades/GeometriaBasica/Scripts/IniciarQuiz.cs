using UnityEngine;

public class IniciarQuiz : MonoBehaviour
{

    ExecutadorQuiz executador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        executador = FindFirstObjectByType<ExecutadorQuiz>();
        executador.iniciarQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
