using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game_controller : MonoBehaviour
{

    private ExecutadorCiclos executor;

    private void Awake() {
        executor = FindFirstObjectByType<ExecutadorCiclos>();
    }

    public void carregaPanel (string nomePanel)
    {
        SceneManager.LoadScene(nomePanel);
    }

    public void btnProximaAtividade() {
        executor.proximaAtividade();
    }




}