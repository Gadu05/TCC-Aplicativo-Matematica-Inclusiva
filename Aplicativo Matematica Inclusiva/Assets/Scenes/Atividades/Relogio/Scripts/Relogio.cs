using System;
using UnityEngine;
using UnityEngine.UI;

public class Relogio : MonoBehaviour
{

    public Text relogioTexto;

    public int hora { private set; get; }
    public int minuto { private set; get; }

    private static Relogio instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this); // <-- destrói apenas o componente duplicado
            return;
        }

        instancia = this;

        GerarHoraMinuto();
        relogioTexto.text = this.hora.ToString("00") + ":" + this.minuto.ToString("00");
        Debug.Log($"[Relogio Awake] Rodando no GameObject: {gameObject.name}");
    }

    public void GerarHoraMinuto()
    {
        int hora = UnityEngine.Random.Range(1, 13);
        int minuto = UnityEngine.Random.Range(0, 12) * 5;
        //int hora = 0;

        relogioTexto.text = this.hora.ToString("00") + ":" + this.minuto.ToString("00");

        DefinirHoraInternamente(hora, minuto);
        Debug.Log($"Hora gerada: {this.hora:D2}:{this.minuto:D2}");
    }

    private void DefinirHoraInternamente(int hora, int minuto)
    {
        this.hora = hora;
        this.minuto = minuto;
    }
}
