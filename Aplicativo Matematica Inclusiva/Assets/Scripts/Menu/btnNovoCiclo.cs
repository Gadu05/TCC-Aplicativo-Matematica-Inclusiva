using UnityEngine;
using UnityEngine.SceneManagement;

public class btnNovoCiclo : MonoBehaviour
{
    public void CriarNovoCiclo()
    {
        Debug.Log("Abrindo cena CriarCiclo...");
        SceneManager.LoadScene("CriarCiclo");
    }


}

