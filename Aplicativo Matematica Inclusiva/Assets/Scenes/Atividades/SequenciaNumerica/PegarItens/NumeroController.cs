using UnityEngine;
using TMPro;

public class NumeroController : MonoBehaviour {
    public TextMeshPro text;
    private int valor;

    public void SetValor(int v) {
        valor = v;
        text.text = v.ToString();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Chao")) {
            Destroy(gameObject);
        }
    }
}
