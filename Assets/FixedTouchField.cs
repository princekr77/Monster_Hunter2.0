using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Touch Field Settings")]
    public float sensitivity = 1f;

    [HideInInspector] public Vector2 TouchDist;
    [HideInInspector] public bool Pressed;
    [HideInInspector] public bool IsDragging;

    private Vector2 pointerOld;
    private int pointerId;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Make sure the image is raycastable
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = true;
        }
    }

    void Update()
    {
        // Handle touch input for mobile
        if (Pressed && pointerId >= 0 && pointerId < Input.touches.Length)
        {
            TouchDist = (Input.touches[pointerId].position - pointerOld) * sensitivity;
            pointerOld = Input.touches[pointerId].position;
        }
        else if (IsDragging)
        {
            // Fallback for mouse input in editor
            Vector2 currentMousePos = Input.mousePosition;
            TouchDist = (currentMousePos - pointerOld) * sensitivity;
            pointerOld = currentMousePos;
        }
        else
        {
            TouchDist = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            pointerOld = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            Pressed = false;
            IsDragging = false;
            TouchDist = Vector2.zero;
        }
    }

    // Helper method to check if touch is within this UI element
    public bool IsTouchInArea(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition);
    }
}