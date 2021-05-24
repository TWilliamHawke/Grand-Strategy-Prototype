using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    public abstract class AbstractItemSlot : UIElementWithTooltip, IPointerClickHandler
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;

        [Header("UI Elements")]
        [SerializeField] Text _itemName;
        [SerializeField] Image _itemIcon;

        protected ItemSlotController itemSlotController => _itemSlotController;
        protected TemplateController templateController => _templateController;

        int _itemCost = 0;

        public int itemCost
        {
            get => _itemCost;
            private set => SetItemCost(value);
        }

        Equipment _itemInSlot;

        public virtual List<Equipment> itemsForSlot => _itemSlotController.weapon;

        protected abstract void AddItemToTemplate(Equipment item);
        protected abstract Equipment SelectItemFromTemplate(UnitTemplate template);

        void OnEnable()
        {
            _itemSlotController.AddItemSlot(this);
            UpdateSlotUI(_templateController.currentTemplate);
            _templateController.OnTemplateChange += UpdateSlotUI;
            _templateController.OnBuildingAdded += UpdateItemCost;
        }

        void OnDisable()
        {
            _templateController.OnTemplateChange -= UpdateSlotUI;
            _templateController.OnBuildingAdded -= UpdateItemCost;

        }

        public override string GetTooltipText()
        {
            return _itemInSlot.GetToolTipText();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if ((int)eventData.button == 0)
            {
                _itemSlotController.SelectItemSlot(this);
            }
            if ((int)eventData.button == 1)
            {
                HideTooltip();
                itemCost = 0;
                AddItemToTemplate(itemsForSlot[0]);
                _templateController.UpdateTemplate();
            }
        }

        public void ChangeItemInSlot(Equipment item)
        {
            itemCost = item.CalculateCurrentCost(_templateController);
            AddItemToTemplate(item);
            _templateController.UpdateTemplate();
        }

        void UpdateItemCost()
        {
            itemCost = _itemInSlot.CalculateCurrentCost(_templateController);
        }

        void SetItemCost(int value)
        {
            _itemCost = value;
            _itemSlotController.UpdateEquipmentCost();
        }

        void UpdateSlotUI(UnitTemplate template)
        {
            var item = SelectItemFromTemplate(template);
            itemCost = item.CalculateCurrentCost(_templateController);

            _itemInSlot = item;
            _itemName.text = item.Name;
            _itemIcon.sprite = item.sprite;
        }
    }
}