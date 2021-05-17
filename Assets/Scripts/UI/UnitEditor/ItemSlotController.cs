using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnitEditor
{
[CreateAssetMenu(fileName = "ItemSlotController", menuName = "Unit Editor/ItemSlotController", order = 101)]
public class ItemSlotController : ScriptableObject
{
    [SerializeField] TemplateController _templatController;

    public event UnityAction<UnityEvent<Equipment>, List<Equipment>> OnItemSlotSelection;
    public Equipment itemInSelectedSlot { get; set; }

    public int freeGold
    {
        get
        {
            int classWealth = _templatController.currentTemplate.unitClass.wealth;
            int totalEquipmentCost = _templatController.currentTemplate.GetEquipmentTotalCost();
            return classWealth - (totalEquipmentCost - itemInSelectedSlot.goldCost);
        }
    }
    public int classSkill => _templatController.currentTemplate.unitClass.weaponSkill;

    ItemSlot _selectedSlot;

    public void SelectItemSlot(UnityEvent<Equipment> callback, List<Equipment> itemList)
    {
        OnItemSlotSelection?.Invoke(callback, itemList);
    }


}


}