using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TooltipController : ScriptableObject
{
    public event UnityAction<string> OnShowTooltip;
    public event UnityAction OnHideTooltip;
    [Range(0, 1)]
    public float delayTimeout;



    public void ShowTooltip(IHaveTooltip tooltipSourse)
    {
        string tooltipText = tooltipSourse.GetTooltipText();
        OnShowTooltip?.Invoke(tooltipText);
    }

    public void HideTooltip()
    {
        OnHideTooltip?.Invoke();
    }
}
