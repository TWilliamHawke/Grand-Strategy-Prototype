using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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

        //getters
        public List<Equipment> weapon => _weapon;
        public List<Equipment> shields => _shields;
        public List<Equipment> armor => _armor;
        public List<Equipment> mounts => _mounts;
        public int classSkill => _templateController.currentTemplate.unitClass.weaponSkill;

        //events
        public event UnityAction<List<Equipment>> OnItemSlotSelection;
        public event UnityAction<int> OnEquipmentCostChange;

        //inner data
        List<AbstractItemSlot> _itemSlots = new List<AbstractItemSlot>();
        AbstractItemSlot _selectedSlot;

        void OnDisable()
        {
            _itemSlots.Clear();
        }

        public void UpdateEquipmentCost()
        {
            int cost = CalculateEquipmentCost();
            OnEquipmentCostChange?.Invoke(cost);
        }

        public void RegisterSlot(AbstractItemSlot slot)
        {
            if(_itemSlots.Contains(slot)) return;

            _itemSlots.Add(slot);
        }

        public void SelectItemSlot(AbstractItemSlot itemSlot)
        {
            _selectedSlot = itemSlot;
            var unlockedItems = _selectedSlot.itemsForSlot.Where(i => i.isUnlocked).ToList();

            OnItemSlotSelection?.Invoke(unlockedItems);
        }

        public void ChangeItemInSelectedSlot(Equipment item)
        {
            _selectedSlot.ChangeItemInSlot(item);
        }

        //calculated here because itemCost is dynamic
        //current cost stored in item slot
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

        public bool ConvertEquipment<T>(Equipment equipment, out T item) where T : Equipment
        {
            if (equipment is T)
            {
                item = (T)equipment;
                return true;
            }
            else
            {
                ReportWrongItem<T>(equipment);
                item = null;
                return false;
            }
        }

        void ReportWrongItem<T>(Equipment equipment)
        {
            string message = $"Wrong item ({ equipment.Name }) in list: { typeof(T) }";
            throw new System.Exception(message);
        }


    }


}