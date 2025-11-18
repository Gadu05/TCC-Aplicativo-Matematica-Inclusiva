using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DragHandleItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [Header("Referências")]
    public RectTransform itemToDrag;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform parentLayout;
    private ScrollRect scrollRect;
    private GameObject placeholder;
    private GameObject indicator;
    private int originalIndex;
    private Vector2 pointerOffset;

    [Header("Auto-scroll")]
    public float scrollSpeed = 12f;
    public float scrollThreshold = 80f;

    void Awake() {
        if (itemToDrag == null)
            itemToDrag = GetComponentInParent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();
        parentLayout = itemToDrag.parent as RectTransform;
        scrollRect = GetComponentInParent<ScrollRect>();

        canvasGroup = itemToDrag.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = itemToDrag.gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originalIndex = itemToDrag.GetSiblingIndex();

        // Cria placeholder invisível para manter espaço no layout
        placeholder = new GameObject("Placeholder");
        var phRect = placeholder.AddComponent<RectTransform>();
        var phLayout = placeholder.AddComponent<LayoutElement>();

        phLayout.preferredHeight = itemToDrag.rect.height;
        phLayout.preferredWidth = itemToDrag.rect.width;

        placeholder.transform.SetParent(parentLayout);
        placeholder.transform.SetSiblingIndex(originalIndex);

        // 🔹 Cria o indicador visual (linha azul)
        indicator = new GameObject("DropIndicator");
        var indRect = indicator.AddComponent<RectTransform>();
        var img = indicator.AddComponent<Image>();
        img.color = new Color(0.2f, 0.6f, 1f, 0.9f); // azul claro
        indRect.sizeDelta = new Vector2(parentLayout.rect.width, 6f);
        indicator.transform.SetParent(parentLayout);
        indicator.SetActive(false);

        // Prepara item para arrastar
        itemToDrag.SetParent(canvas.transform);
        itemToDrag.SetAsLastSibling();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            itemToDrag, eventData.position, canvas.worldCamera, out pointerOffset);

        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPoint)) {
            itemToDrag.position = canvas.transform.TransformPoint(localPoint - pointerOffset);
        }

        AutoScroll(eventData);
        UpdatePlaceholderAndIndicator(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        // 🔹 Recoloca o item no lugar do placeholder
        itemToDrag.SetParent(parentLayout);
        itemToDrag.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // 🔹 Limpeza
        Destroy(placeholder);
        Destroy(indicator);

        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout);
    }

    private void UpdatePlaceholderAndIndicator(PointerEventData eventData) {
        int targetIndex = parentLayout.childCount - 1;

        for (int i = 0; i < parentLayout.childCount; i++) {
            var child = parentLayout.GetChild(i);
            if (child == placeholder.transform || child == indicator.transform)
                continue;

            if (eventData.position.y > child.position.y) {
                targetIndex = i;
                break;
            }
        }

        // 🔹 Evita flicker: só atualiza se realmente mudou
        if (placeholder.transform.GetSiblingIndex() != targetIndex) {
            placeholder.transform.SetSiblingIndex(targetIndex);
            indicator.transform.SetSiblingIndex(targetIndex);
        }

        indicator.SetActive(true);

        // 🔹 Garante que o layout recalque o tamanho do Content
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout);
    }


    private void AutoScroll(PointerEventData eventData) {
        if (scrollRect == null) return;

        RectTransform viewport = scrollRect.viewport;
        Vector2 localPointer;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(viewport, eventData.position, canvas.worldCamera, out localPointer))
            return;

        float upper = viewport.rect.height / 2 - scrollThreshold;
        float lower = -viewport.rect.height / 2 + scrollThreshold;
        float delta = 0f;

        if (localPointer.y > upper)
            delta = (localPointer.y - upper) / scrollThreshold;
        else if (localPointer.y < lower)
            delta = (localPointer.y - lower) / scrollThreshold;

        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(
            scrollRect.verticalNormalizedPosition + delta * scrollSpeed * Time.deltaTime
        );
    }
}
