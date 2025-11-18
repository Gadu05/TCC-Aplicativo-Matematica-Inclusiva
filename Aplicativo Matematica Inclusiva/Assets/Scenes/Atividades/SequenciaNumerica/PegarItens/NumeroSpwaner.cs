using UnityEngine;

public class NumeroSpawner : MonoBehaviour {
    public GameObject numeroPrefab;
    public float intervalo = 1.5f;
    public float minX = -2f, maxX = 2f;
    public float posY = 6f;

    private void Start() {
        InvokeRepeating(nameof(GerarNumero), 0f, intervalo);
    }

    void GerarNumero() {
        Vector3 pos;
        bool spawnValido = false;

        // Tenta spawnar até não colidir com player
        int tentativas = 0;
        do {
            pos = new Vector3(Random.Range(minX, maxX), posY, 0f);
            Collider2D hit = Physics2D.OverlapCircle(pos, 0.5f); // verifica se há algo no spawn
            if (hit == null || !hit.CompareTag("Player")) {
                spawnValido = true;
            }
            tentativas++;
        } while (!spawnValido && tentativas < 10);

        GameObject num = Instantiate(numeroPrefab, pos, Quaternion.identity);
        int valor = Random.Range(1, 10);
        num.GetComponent<NumeroController>().SetValor(valor);
    }


}
