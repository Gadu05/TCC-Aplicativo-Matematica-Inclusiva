using UnityEngine;
using UnityEngine.UI;

public class PainelFinal : MonoBehaviour
{
    private int idCenario;
    public Text txtNota_final;

    private void Start()
    {
        idCenario = PlayerPrefs.GetInt("idCenario", 0);

        float acertos = PlayerPrefs.GetFloat("certo" + idCenario.ToString(), 0);
        int notaFinal = Mathf.RoundToInt(acertos);

        txtNota_final.text = "Nota: " + notaFinal.ToString();

        if (notaFinal > PlayerPrefs.GetInt("NotaFinal" + idCenario.ToString(), 0))
        {
            PlayerPrefs.SetInt("NotaFinal" + idCenario.ToString(), notaFinal);
        }

        
        PlayerPrefs.DeleteKey("certo" + idCenario.ToString());
    }
}
