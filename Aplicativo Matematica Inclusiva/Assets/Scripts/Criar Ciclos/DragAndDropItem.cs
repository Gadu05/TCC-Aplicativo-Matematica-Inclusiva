using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;
    private int originalSiblingIndex;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = FindCanvas(transform);

        if (canvas == null)
            Debug.LogError($"Canvas não encontrado para {gameObject.name}.");
    }

    private Canvas FindCanvas(Transform t) {
        if (t == null) return null;
        Canvas c = t.GetComponent<Canvas>();
        if (c != null) return c;
        return FindCanvas(t.parent);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (canvas == null) return;

        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Move para o topo do Canvas
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        if (canvas == null) return;

        // Move o item com o ponteiro
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (canvas == null) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Volta para o pai original
        transform.SetParent(originalParent);

        // Reordena baseado na posição do ponteiro
        ReorderInLayout(eventData);
    }

    private void ReorderInLayout(PointerEventData eventData) {
        int newIndex = originalParent.childCount;

        // Percorre todos os filhos do layout
        for (int i = 0; i < originalParent.childCount; i++) {
            RectTransform child = originalParent.GetChild(i) as RectTransform;
            if (child == rectTransform) continue;

            // Se o ponteiro estiver acima do centro do item, insere antes dele
            Vector3 worldPos = child.position;
            if (eventData.position.y > worldPos.y) {
                newIndex = i;
                break;
            }
        }

        transform.SetSiblingIndex(newIndex);
    }
}
