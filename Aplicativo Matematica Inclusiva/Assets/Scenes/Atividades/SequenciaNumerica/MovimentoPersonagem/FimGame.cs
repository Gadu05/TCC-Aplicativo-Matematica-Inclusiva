using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FimGame : MonoBehaviour
{
    public GameObject gameFim;
    public static FimGame fim;

    private ExecutadorCiclos executor;

    public Text conTexto;
    public int sScore;
    private void Awake()
    {
        executor = FindFirstObjectByType<ExecutadorCiclos>();
        fim = this;
        sScore = 0;
        Time.timeScale = 1;
    }
    
    public void ShowGameOver ()
    {
        gameFim.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        executor.proximaAtividade();
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
    }

    public void AddScore()
    {
        sScore++;
        conTexto.text = sScore.ToString();

        if (sScore > 30) {
            ShowGameOver();
        }

    }


}

