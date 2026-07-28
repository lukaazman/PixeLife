using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextScaler : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // Assign in Inspector
    public RectTransform rectTransform;  // Assign in Inspector if you want to scale buttons too
    public float scaleMultiplier = 1.2f;
    public float transitionDuration = 0.2f;

    private Vector3 originalScale;
    private bool isHovered = false;

    void Start()
    {
        if (textMeshPro != null)
        {
            originalScale = textMeshPro.transform.localScale;
        }
        else if (rectTransform != null)
        {
            originalScale = rectTransform.localScale;
        }
        else
        {
            Debug.LogWarning("Neither TextMeshPro nor RectTransform is assigned!");
        }
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector2 mousePosition = Input.mousePosition;

        // Check if the mouse is over the RectTransform's bounds
        if (IsMouseOverUIElement(mousePosition))
        {
            isHovered = true;
        }
        else
        {
            isHovered = false;
        }

        // Smoothly scale based on whether the mouse is over the UI element
        if (isHovered)
        {
            if (textMeshPro != null)
                textMeshPro.transform.localScale = Vector3.Lerp(textMeshPro.transform.localScale, originalScale * scaleMultiplier, transitionDuration);
            else if (rectTransform != null)
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originalScale * scaleMultiplier, transitionDuration);
        }
        else
        {
            if (textMeshPro != null)
                textMeshPro.transform.localScale = Vector3.Lerp(textMeshPro.transform.localScale, originalScale, transitionDuration);
            else if (rectTransform != null)
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originalScale, transitionDuration);
        }
    }

    // Custom function to check if the mouse is over the RectTransform area
    bool IsMouseOverUIElement(Vector2 mousePosition)
    {
        Vector2 localMousePosition;

        // If we have a TextMeshPro, use its RectTransform for calculations
        if (textMeshPro != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(textMeshPro.rectTransform, mousePosition, null, out localMousePosition);
            return textMeshPro.rectTransform.rect.Contains(localMousePosition);
        }
        // If we're scaling a button or other UI object, use the rectTransform
        else if (rectTransform != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localMousePosition);
            return rectTransform.rect.Contains(localMousePosition);
        }
        return false;
    }
}
