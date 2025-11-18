using UnityEngine;
using TMPro;

public class PegarItem : MonoBehaviour {
    [Header("Referências")]
    public GameObject numObj;   // Prefab do número
    public GameObject player;   // Player

    private FimGame fimGame;
    private Camera cam;
    private int contador = 1;
    public float variacaoX = 1.5f;

    void Start() {
        cam = Camera.main;
        fimGame = FindFirstObjectByType<FimGame>();

        // Debug inicial
        Debug.Log($"[PegarItem] Ativo em {gameObject.name}. Tem Collider? {GetComponent<Collider2D>() != null}");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Colidiu com: " + collision.gameObject.name);

        if (!collision.gameObject.CompareTag("Player")) return;

        if (contador >= 10) {
            fimGame?.ShowGameOver();
            return;
        }

        contador++;

        // Calcula spawn dentro dos limites da câmera
        Vector3 esquerda = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 direita = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float xSpawn = Mathf.Clamp(
            cam.transform.position.x + Random.Range(-variacaoX, variacaoX),
            esquerda.x + 0.5f,
            direita.x - 0.5f
        );

        Vector3 posicaoAleatoria = new Vector3(xSpawn, cam.transform.position.y + 6f, 0);

        // Cria o número na tela
        GameObject numeroInst = Instantiate(numObj, posicaoAleatoria, Quaternion.identity);

        TMP_Text texto = numeroInst.GetComponentInChildren<TMP_Text>();
        if (texto != null) {
            texto.text = contador.ToString();
        } else {
            Debug.LogWarning("TMP_Text não encontrado no prefab NumObj.");
        }

        // Destroi o item que foi coletado
        Destroy(gameObject);
    }
}
