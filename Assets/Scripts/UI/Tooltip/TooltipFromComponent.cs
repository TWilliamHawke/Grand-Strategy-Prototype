using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipFromComponent : MonoBehaviour, IHaveTooltip, IPointerClickHandler
{
    [SerializeField] TooltipController _tooltipController;
    [SerializeField] bool _hideTooltipOnClick = false;

    ITooltipComponent _component;

    void Awake()
    {
        if(!TryGetComponent<ITooltipComponent>(out _component))
        {
            Debug.LogError("ITooltipComponent doesn't find");
        }
    }

    public string GetTooltipText()
    {
        return _component?.GetTooltipText() ?? "NO TOOLTIP COMPONENT";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_hideTooltipOnClick)
        {
            _tooltipController.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_component != null)
        {
            _tooltipController.ShowTooltip(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipController.HideTooltip();
    }

    public void HideTooltip()
    {
        _tooltipController.HideTooltip();
    }

    public void ShowTooltip()
    {
        _tooltipController.ShowTooltip(this);
    }


}
