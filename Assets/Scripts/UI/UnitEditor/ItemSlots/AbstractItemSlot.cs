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

        public int itemCost { get; private set; }

        Equipment _itemInSlot;

        public virtual List<Equipment> itemsForSlot => _itemSlotController.weapon;

        public abstract void AddItemToTemplate(Equipment item);
        protected abstract Equipment SelectItemFromTemplate(UnitTemplate template);

        void OnEnable()
        {
            _itemSlotController.AddItemSlot(this);
            UpdateItemInSlot(_templateController.currentTemplate);
            _templateController.OnTemplateChange += UpdateItemInSlot;
        }

        void OnDisable()
        {
            _templateController.OnTemplateChange -= UpdateItemInSlot;
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
            }
        }

        void UpdateItemInSlot(UnitTemplate template)
        {
            var item = SelectItemFromTemplate(template);
            itemCost = item.CalculateCurrentCost(_templateController);
            UpdateItemInSlot(item);
        }

        void UpdateItemInSlot(Equipment item)
        {
            _itemInSlot = item;
            _itemName.text = item.Name;
            _itemIcon.sprite = item.sprite;
        }
    }
}