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

        [SerializeField] List<Equipment> _weapon;
        [SerializeField] List<Equipment> _shields;
        [SerializeField] List<Equipment> _armor;
        [SerializeField] List<Equipment> _mounts;

        public List<Equipment> weapon => _weapon;
        public List<Equipment> shields => _shields;
        public List<Equipment> armor => _armor;
        public List<Equipment> mounts => _mounts;

        public event UnityAction<List<Equipment>> OnItemSlotSelection;
        public event UnityAction<int> OnEquipmentCostChange;

        public int classSkill => _templateController.currentTemplate.unitClass.weaponSkill;

        List<AbstractItemSlot> _itemSlots = new List<AbstractItemSlot>();
        AbstractItemSlot _selectedSlot;

        private void OnDisable()
        {
            _itemSlots.Clear();
        }

        public void UpdateEquipmentCost()
        {
            int cost = CalculateEquipmentCost();
            OnEquipmentCostChange?.Invoke(cost);
        }

        public void AddItemSlot(AbstractItemSlot slot)
        {
            _itemSlots.Add(slot);
        }

        public void SelectItemSlot(AbstractItemSlot itemSlot)
        {
            _selectedSlot = itemSlot;
            OnItemSlotSelection?.Invoke(_selectedSlot.itemsForSlot);
        }

        public void ChangeItemInSelectedSlot(Equipment item)
        {
            _selectedSlot.ChangeItemInSlot(item);
        }

        public int CalculateFreeGold()
        {
            var gold = _templateController.realwealth;
            foreach (var itemSlot in _itemSlots)
            {
                if (itemSlot == _selectedSlot) continue;

                gold -= itemSlot.itemCost;
            }

            return gold;
        }

        int CalculateEquipmentCost()
        {
            int gold = 0;

            foreach (var itemSlot in _itemSlots)
            {
                gold += itemSlot.itemCost;
            }

            return gold;
        }

    }


}