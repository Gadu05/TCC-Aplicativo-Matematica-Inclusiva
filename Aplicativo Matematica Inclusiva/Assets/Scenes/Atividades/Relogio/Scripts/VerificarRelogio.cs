using TMPro;
using UnityEngine;

public class VerificarRelogio : MonoBehaviour {
    [Header("Ponteiro do relogio")]
    public Transform pontHoras;
    public Transform pontMinutos;

    [Header("Relogio: Hora certa")]
    public int horaCerto;
    public int minutosCerto;

    [Header("Feedback")]
    public TextMeshProUGUI textoFinal;

    [Header("Texto do relógio")]
    public TextMeshProUGUI relogioTexto;

    [Header("Referência para script Relogio")]
    public Relogio relogio;  // <-- Referência ao outro script

    private ExecutadorCiclos executor;

    public void Awake() {
        //relogio.GerarHoraMinuto();
    }

    public void VerificarResposta() {
        int selecionaHora = ObterHora();
        int selecionaMinuto = ObterMinuto();

        // Pega a hora/minuto que o botão já gerou
        horaCerto = (relogio.hora % 12);
        minutosCerto = relogio.minuto;

        if (selecionaHora == horaCerto && selecionaMinuto == minutosCerto) {
            //textoFinal.text = "Muito bom!";
            executor = FindFirstObjectByType<ExecutadorCiclos>();
            executor.proximaAtividade();
        } else {
            textoFinal.text = "TENTE NOVAMENTE!";
        }

        Debug.Log($"Selecionado: {selecionaHora:D2}:{selecionaMinuto:D2} | Correto: {horaCerto:D2}:{minutosCerto:D2}");
    }

    int ObterHora() {
        float direcaoZ = pontHoras.localEulerAngles.z;
        float anguloHorario = (360f - direcaoZ + 90f) % 360f;
        int hora = Mathf.RoundToInt((anguloHorario) / 30f) % 12;
        return hora;
    }

    int ObterMinuto() {
        float direcaoZ = pontMinutos.localEulerAngles.z;
        int minuto = Mathf.RoundToInt((360f - direcaoZ) / 6f) % 60;
        return minuto;
    }
}
