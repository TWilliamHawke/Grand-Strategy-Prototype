using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace UnitEditor
{

    public class ItemList : UIPanelWithGrid<Equipment>
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;

        protected override List<Equipment> _layoutElementsData => _itemList;

        List<Equipment> _itemList = new List<Equipment>();

        private void OnEnable()
        {
            _itemSlotController.OnItemSlotSelection += UpdateItemList;
            _templateController.OnBuildingAdded += UpdateLayout;
        }

        private void OnDisable()
        {
            _itemSlotController.OnItemSlotSelection -= UpdateItemList;
            _templateController.OnBuildingAdded -= UpdateLayout;
        }

        void UpdateItemList(List<Equipment> equipmentList)
        {
            _itemList = equipmentList;
            UpdateLayout();
        }

    }
}