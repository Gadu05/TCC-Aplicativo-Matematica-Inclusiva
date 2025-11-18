using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GirarPonteiroMinutoUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [Tooltip("Texto que aparecerá quaso erre!")]
    [SerializeField]
    private TMP_Text resultado;

    [Tooltip("Centro do relógio (ex: objeto vazio no meio)")]
    public Transform centroRotacao;

    private Outline outline;

    // Variáveis para armazenar o offset angular no início do drag
    private float anguloInicialPonteiro;
    private float anguloInicialMouse;

    void Awake() {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {

        resultado.text = "";


        if (Camera.main == null) return;

        if (outline != null)
            outline.enabled = true;

        Vector2 posMundo = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 direcaoMouse = posMundo - (Vector2)centroRotacao.position;

        // Ângulo do mouse no início do drag
        anguloInicialMouse = Mathf.Atan2(direcaoMouse.y, direcaoMouse.x) * Mathf.Rad2Deg;

        // Ângulo atual do ponteiro
        anguloInicialPonteiro = transform.eulerAngles.z;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Camera.main == null) return;

        Vector2 posMundo = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 direcaoMouse = posMundo - (Vector2)centroRotacao.position;

        // Ângulo atual do mouse
        float anguloAtualMouse = Mathf.Atan2(direcaoMouse.y, direcaoMouse.x) * Mathf.Rad2Deg;

        // Diferença do movimento do mouse desde o início do drag
        float deltaAnguloMouse = Mathf.DeltaAngle(anguloInicialMouse, anguloAtualMouse);

        // Calcula o novo ângulo do ponteiro somando o delta
        float novoAngulo = anguloInicialPonteiro + deltaAnguloMouse;

        transform.rotation = Quaternion.Euler(0, 0, novoAngulo);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (outline != null)
            outline.enabled = false;

        // Snap a cada 30 graus (5 minutos / 1 hora)
        float zAtual = transform.eulerAngles.z;
        float zCorrigido = (zAtual + 360f) % 360f;
        float zSnapped = Mathf.Round(zCorrigido / 30f) * 30f;

        transform.rotation = Quaternion.Euler(0, 0, zSnapped);
    }
}
