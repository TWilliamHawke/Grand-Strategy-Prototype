using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Effects;

namespace UnitEditor
{
[CreateAssetMenu(fileName = "ItemSlotController", menuName = "Unit Editor/ItemSlotController", order = 101)]
public class ItemSlotController : ScriptableObject
{
    [SerializeField] TemplateController _templateController;

    public event UnityAction<UnityEvent<Equipment>, List<Equipment>> OnItemSlotSelection;
    public Equipment itemInSelectedSlot { get; set; }

    public int freeGold
    {
        get
        {
            int classWealth = _templateController.currentTemplate.unitClass.wealth;
            int totalEquipmentCost = _templateController.currentTemplate.GetEquipmentTotalCost();
            return classWealth - (totalEquipmentCost - itemInSelectedSlot.goldCost);
        }
    }
    public int classSkill => _templateController.currentTemplate.unitClass.weaponSkill;

    ItemSlot _selectedSlot;

    public void SelectItemSlot(UnityEvent<Equipment> callback, List<Equipment> itemList)
    {
        var effects = _templateController.FindAllEffects<Effect>();
        foreach(var effect in effects)
        {
            Debug.Log(effect.GetText());
        }
        OnItemSlotSelection?.Invoke(callback, itemList);
    }


}


}