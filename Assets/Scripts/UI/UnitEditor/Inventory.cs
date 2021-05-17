using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
public class Inventory : MonoBehaviour
{
    [SerializeField] TemplateController _templateController;
    [SerializeField] ItemSlot _primaryWeaponSlot;
    [SerializeField] ItemSlot _secondaryWeaponSlot;
    [SerializeField] ItemSlot _shieldSlot;
    [SerializeField] ItemSlot _armorSlot;
    [SerializeField] ItemSlot _mountSlot;

    void OnEnable()
    {
        UpdateSlots(_templateController.currentTemplate);
        _templateController.OnTemplateChange += UpdateSlots;
    }

    void OnDisable()
    {
        _templateController.OnTemplateChange -= UpdateSlots;
    }


    void UpdateSlots(UnitTemplate template)
    {
        _primaryWeaponSlot.UpdateItemInSlot(template.primaryWeapon);
        _secondaryWeaponSlot.UpdateItemInSlot(template.secondaryWeapon);
        _shieldSlot.UpdateItemInSlot(template.shield);
        _armorSlot.UpdateItemInSlot(template.armour);
        _mountSlot.UpdateItemInSlot(template.mount);
    }
}

}