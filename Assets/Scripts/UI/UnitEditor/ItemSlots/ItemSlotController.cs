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
        public event UnityAction OnItemReset;

        //inner data
        Dictionary<string, AbstractItemSlot> _itemSlots = new Dictionary<string, AbstractItemSlot>();
        AbstractItemSlot _selectedSlot;

        void OnDisable()
        {
            _itemSlots.Clear();
        }

        public void UpdateEquipmentCost()
        {
            int cost = _itemSlots.Sum(slot => slot.Value.itemCost);
            OnEquipmentCostChange?.Invoke(cost);
        }

        public void RegisterSlot(AbstractItemSlot slot)
        {
            _itemSlots.Add(slot.GetType().FullName, slot);
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
            return CalculateFreeGoldWithoutSlot(_selectedSlot);
        }

        public int CalculateFreeGoldWithoutSlot(AbstractItemSlot currentSlot)
        {
            var gold = _templateController.realwealth;

            foreach (var itemSlot in _itemSlots.Values)
            {
                if (itemSlot == currentSlot) continue;

                gold -= itemSlot.itemCost;
            }

            return gold;
        }

        public void ResetItem()
        {
            OnItemReset?.Invoke();
        }

    }

}