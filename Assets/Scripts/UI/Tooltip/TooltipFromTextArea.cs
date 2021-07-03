using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipFromTextArea : MonoBehaviour, IHaveTooltip, IPointerClickHandler
{
    [SerializeField] TooltipController _tooltipController;
    [TextArea]
    [SerializeField] string _tooltipText;
    [SerializeField] bool _hideTooltipOnClick = false;


    public string GetTooltipText()
    {
        
        return _tooltipText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_hideTooltipOnClick)
        {
            _tooltipController.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipController.ShowTooltip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipController.HideTooltip();
    }
}
