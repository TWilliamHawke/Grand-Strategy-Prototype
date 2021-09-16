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
        Dictionary<EquipmentSlots, AbstractItemSlot> _itemSlots = new Dictionary<EquipmentSlots, AbstractItemSlot>();
        Dictionary<EquipmentSlots, List<Equipment>> _equpiment;
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
            if (_equpiment == null)
            {
                FillEquipmentList();
            }

            _itemSlots.Add(slot.slotType, slot);
            if (_equpiment.TryGetValue(slot.slotType, out var itemList))
            {
                slot.SetDefaultItem(itemList[0]);
            }
            else
            {
                slot.SetDefaultItem(_weapon[0]);
            }
        }

        public void SelectItemSlot(AbstractItemSlot itemSlot)
        {
            _selectedSlot = itemSlot;
            if (_equpiment.TryGetValue(itemSlot.slotType, out var items))
            {
                var unlockedItems = items.Where(i => i.isUnlocked).ToList();
                OnItemSlotSelection?.Invoke(unlockedItems);
            }
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

        void FillEquipmentList()
        {
            _equpiment = new Dictionary<EquipmentSlots, List<Equipment>>()
            {
                { EquipmentSlots.primaryWeapon, _weapon },
                { EquipmentSlots.secondaryWeapon, _weapon },
                { EquipmentSlots.armour, _armor },
                { EquipmentSlots.shield, _shields },
                { EquipmentSlots.mount, _mounts },
            };
        }



    }

}