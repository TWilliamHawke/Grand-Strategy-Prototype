using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIElementWithTooltip : MonoBehaviour, IHaveTooltip
{
    [SerializeField] TooltipController _tooltipController;

    public abstract string GetTooltipText();

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipController.ShowTooltip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
    
    protected void HideTooltip()
    {
        _tooltipController.HideTooltip();
    }

    protected void ShowTooltip()
    {
        _tooltipController.ShowTooltip(this);
    }
}
