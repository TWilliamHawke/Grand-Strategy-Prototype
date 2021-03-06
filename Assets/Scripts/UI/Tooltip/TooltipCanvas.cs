using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipCanvas : MonoBehaviour
{
    [SerializeField] RectTransform _tooltip;
    [SerializeField] Text _tooltipText;
    [SerializeField] TooltipController _tooltipController;

    VerticalLayoutGroup _tooltipLayout;

    bool _tooltipIsActive = false;
    Coroutine _delayCoroutine;

    void Awake()
    {
        _tooltipLayout = _tooltip.GetComponent<VerticalLayoutGroup>();
    }

    private void OnEnable()
    {
        _tooltipController.OnShowTooltip += ShowTooltip;
        _tooltipController.OnHideTooltip += HideTooltip;
    }

    void OnDisable()
    {
        _tooltipController.OnShowTooltip -= ShowTooltip;
        _tooltipController.OnHideTooltip -= HideTooltip;
    }

    void Update()
    {
        //TODO fix tooltip position near screen borders
        if (_tooltipIsActive)
        {
            SetTooltipPosition();
        }
    }

    void ShowTooltip(string text)
    {
        _tooltipText.text = text;
        _tooltipLayout.enabled = false;
        _delayCoroutine = StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(_tooltipController.delayTimeout);
        _tooltipIsActive = true;
        SetTooltipPosition();
        _tooltip.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_tooltip);
        _tooltipLayout.enabled = true;
    }

    void HideTooltip()
    {
        StopCoroutine(_delayCoroutine);
        _tooltipIsActive = false;
        _tooltip.gameObject.SetActive(false);
    }

    void SetTooltipPosition()
    {
        Vector2 offset = _tooltip.sizeDelta / 2;
        _tooltip.position = Input.mousePosition + (Vector3)offset;

        if(Input.mousePosition.x + _tooltip.sizeDelta.x > Screen.width)
        {
            _tooltip.position = new Vector3(Screen.width - _tooltip.sizeDelta.x / 2, _tooltip.position.y, 0);
        }
    }
}
