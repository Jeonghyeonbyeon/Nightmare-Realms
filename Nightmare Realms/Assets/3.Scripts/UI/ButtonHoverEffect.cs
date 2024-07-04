using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text buttonText;
    [SerializeField] private Color hoverColor = Color.black;
    [SerializeField] private float hoverScale = 1.2f;

    private Color originalColor;
    private Vector3 originalScale;
    private FontStyle originalFontStyle;

    void Start()
    {
        originalColor = buttonText.color;
        originalScale = buttonText.transform.localScale;
        originalFontStyle = buttonText.fontStyle;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
        buttonText.transform.localScale = originalScale * hoverScale;
        buttonText.fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
        buttonText.transform.localScale = originalScale;
        buttonText.fontStyle = originalFontStyle;
    }
}
