using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    [RequireComponent(typeof(TooltipFromComponent))]
    public abstract class AbstractItemSlot : MonoBehaviour,
                IPointerClickHandler, INeedInit, ITooltipComponent
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;

        [Header("UI Elements")]
        [SerializeField] Text _itemName;
        [SerializeField] Image _itemIcon;

        //getters
        protected ItemSlotController itemSlotController => _itemSlotController;
        protected TemplateController templateController => _templateController;

        TooltipFromComponent _tooltip;

        int _itemCost = 0;

        public int itemCost
        {
            get => _itemCost;
            //HACK require normal logic
            private set => SetItemCost(value);
        }

        Equipment _itemInSlot;

        //abstract
        public virtual List<Equipment> itemsForSlot => _itemSlotController.weapon;
        protected abstract void AddItemToTemplate(Equipment item);
        protected abstract Equipment SelectItemFromTemplate(UnitTemplate template);

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
                ResetItemInSlot();
            }
        }

        public void ChangeItemInSlot(Equipment item)
        {
            itemCost = _templateController.FindRealItemCost(item);
            AddItemToTemplate(item);
            _templateController.UpdateTemplate();
        }

        void ResetItemInSlot()
        {
            _tooltip.HideTooltip();
            itemCost = 0;
            AddItemToTemplate(itemsForSlot[0]);
            _templateController.UpdateTemplate();
            _itemSlotController.ResetItem();
            _tooltip.ShowTooltip();
        }

        void CheckItemRequirement()
        {
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

        void UpdateSlotUI(UnitTemplate template)
        {
            var item = SelectItemFromTemplate(template);
            itemCost = _templateController.FindRealItemCost(item);

            _itemInSlot = item;
            _itemName.text = item.Name;
            _itemIcon.sprite = item.sprite;
        }
    }
}