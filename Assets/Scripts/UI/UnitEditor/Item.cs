using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace UnitEditor
{
    public class Item : UIElementWithTooltip, IPointerClickHandler
{
    [SerializeField] ItemSlotController _itemSlotController;
    [Header("UI Elements")]
    [SerializeField] Image _itemIcon;
    [SerializeField] Text _itemName;
    [SerializeField] Text _itemCost;
    [SerializeField] Text _weaponSkillText;
    [SerializeField] RectTransform _weaponSkill;

    Equipment _itemData;
    string _tooltipText;
    bool _isInactive = false;

    public Equipment itemData => _itemData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_itemData == null || _isInactive) return;

        _itemSlotController?.ChangeItemInSelectedSlot(_itemData);

    }

    public void SetItemData(Equipment item)
    {
        _itemData = item;
        _itemIcon.sprite = item.sprite;
        _itemName.text = item.Name;
        _itemCost.text = item.goldCost.ToString();
        _tooltipText = _itemData.GetToolTipText();
        SetWeaponSkill();
    }

    private void SetWeaponSkill()
    {
        if(_itemData is RequireSkillEquipment)
        {
            _weaponSkillText.text = (_itemData as RequireSkillEquipment)?.requiredSkill.ToString();
        }
        else
        {
            _weaponSkill.gameObject.SetActive(false);
        }
    }

    public override string GetTooltipText()
    {
        return _tooltipText;
    }

    public void SetInactiveDueGold()
    {
        SetInactive();
        _tooltipText = "<color=red>Not enough gold</color>";
        _itemCost.color = Color.red;
    }

    public void SetInactiveDueSkill()
    {
        SetInactive();
        _tooltipText = "<color=red>Weapon skill is too low</color>";
        _weaponSkillText.color = Color.red;
    }

    void SetInactive()
    {
        _isInactive = true;
        _itemName.color = Color.red;
    }

}

}
