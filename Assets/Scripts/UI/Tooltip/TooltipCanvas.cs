using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipCanvas : MonoBehaviour
{
    [SerializeField] RectTransform _tooltip;
    [SerializeField] Text _tooltipText;
    [SerializeField] TooltipController _tooltipController;

    bool _isTooltipActive = false;
    Coroutine _delayCoroutine;

    private void OnEnable() {
        _tooltipController.OnShowTooltip += ShowTooltip;
        _tooltipController.OnHideTooltip += HideTooltip;
    }

    void OnDisable()
    {
        _tooltipController.OnShowTooltip -= ShowTooltip;
        _tooltipController.OnHideTooltip -= HideTooltip;
    }

    void ShowTooltip(string text)
    {
        _tooltipText.text = text;
        _delayCoroutine = StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(_tooltipController.delayTimeout);
        _isTooltipActive = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tooltip);
        SetTooltipPosition();
        _tooltip.gameObject.SetActive(true);
    }

    void HideTooltip()
    {
        StopCoroutine(_delayCoroutine);
        _isTooltipActive = false;
        _tooltip.gameObject.SetActive(false);
    }

    private void Update() {
        //TODO fix tooltip position near screen borders
        if(_isTooltipActive)
        {
            SetTooltipPosition();
        }
    }

    private void SetTooltipPosition()
    {
        var offset = _tooltip.sizeDelta / 2;
        _tooltip.position = Input.mousePosition + (Vector3)offset;
    }
}
