using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnitCard : UIDataElement<Unit>, IPointerClickHandler
{
    public static event UnityAction<UnitCard> OnSelect;
    public static event UnityAction<UnitCard> OnDeSelect;

    [SerializeField] UnitTemplate _unittemplate;
    [SerializeField] Image _unitIcon;
    [SerializeField] Image _frame;

    public Unit unit { get; private set; }

    public bool isSelected { get; private set; } = false;

    public override void UpdateData(Unit unit)
    {
        this.unit = unit;
        _unittemplate = unit.unitTemplate;
        _unitIcon.sprite = _unittemplate.unitClass.defaultIcon;
    }

    public override string GetTooltipText()
    {
        return _unittemplate.templateName;
    }

    public void Select()
    {
        isSelected = true;
        _frame.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        isSelected = false;
        _frame.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected)
        {
            Deselect();
            OnDeSelect?.Invoke(this);
        }
        else
        {
            Select();
            OnSelect?.Invoke(this);
        }
    }
}
