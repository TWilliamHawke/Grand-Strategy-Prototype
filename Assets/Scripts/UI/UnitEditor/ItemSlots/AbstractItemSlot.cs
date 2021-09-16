using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    [RequireComponent(typeof(TooltipFromComponent))]
    public class AbstractItemSlot : MonoBehaviour, IPointerClickHandler, INeedInit, ITooltipComponent
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;
        [SerializeField] EquipmentSlots _slotType;

        [Header("UI Elements")]
        [SerializeField] Text _itemName;
        [SerializeField] Image _itemIcon;

        //getters
        public EquipmentSlots slotType => _slotType;

        TooltipFromComponent _tooltip;
        Equipment _defaultEquipment;

        int _itemCost = 0;

        public int itemCost
        {
            get => _itemCost;
            //HACK require normal logic
            private set => SetItemCost(value);
        }

        protected Equipment _itemInSlot;

        void OnDestroy()
        {
            _templateController.OnTemplateChange -= UpdateSlotUI;
            _templateController.OnWeaponSkillChange -= CheckItemRequirement;
            _templateController.OnBuildingsChange -= CheckItemRequirement;
        }

        public void Init()
        {
            _tooltip = GetComponent<TooltipFromComponent>();
            _templateController.OnTemplateChange += UpdateSlotUI;
            _templateController.OnWeaponSkillChange += CheckItemRequirement;
            _templateController.OnBuildingsChange += CheckItemRequirement;
            _itemSlotController.RegisterSlot(this);
        }

        public string GetTooltipText()
        {
            if (_itemInSlot == null) return "Item not found!!";

            string helpText = "\n\nRight click for clear slot";
            return _itemInSlot.GetToolTipText() + helpText;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if ((int)eventData.button == 0)
            {
                _itemSlotController.SelectItemSlot(this);
            }
            if ((int)eventData.button == 1)
            {
                _tooltip.HideTooltip();
                ResetItemInSlot();
                _tooltip.ShowTooltip();
            }
        }

        public void ChangeItemInSlot(Equipment item)
        {
            itemCost = _templateController.FindRealItemCost(item);
            _templateController.currentTemplate.inventory[_slotType] = item;

            _templateController.UpdateTemplate();
        }

        public void SetDefaultItem(Equipment item)
        {
            _defaultEquipment = item;
        }

        protected virtual void UpdateSlotUI(UnitTemplate template)
        {
            var item = template.inventory[_slotType];
            itemCost = _templateController.FindRealItemCost(item);

            _itemInSlot = item;
            _itemName.text = item.Name;
            _itemIcon.sprite = item.sprite;
        }


        void ResetItemInSlot()
        {
            itemCost = 0;

            _templateController.currentTemplate.inventory[_slotType] = _defaultEquipment;
            _templateController.UpdateTemplate();
            _itemSlotController.ResetItem();
        }

        void CheckItemRequirement()
        {
            //it possible in dynamic item slot
            if (_itemInSlot == null) return;

            int updatedItemCost = _templateController.FindRealItemCost(_itemInSlot);
            if (itemCost != updatedItemCost)
            {
                bool itemIsTooExpensive = updatedItemCost > _itemSlotController.CalculateFreeGoldWithoutSlot(this);

                if (itemIsTooExpensive)
                {
                    ResetItemInSlot();
                    return;
                }
                else
                {
                    itemCost = updatedItemCost;
                }
            }

            int requiredSkill = _templateController.FindRealSkillForItem(_itemInSlot);
            if (requiredSkill == 0) return;

            int classSkill = _itemInSlot is RangeWeapon ?
                _templateController.currentTemplate.rangeSkill :
                _templateController.currentTemplate.meleeSkill;

            if (requiredSkill > classSkill)
            {
                ResetItemInSlot();
            }
        }

        // 
        void SetItemCost(int value)
        {
            _itemCost = value;
            _itemSlotController.UpdateEquipmentCost();
        }

    }
}