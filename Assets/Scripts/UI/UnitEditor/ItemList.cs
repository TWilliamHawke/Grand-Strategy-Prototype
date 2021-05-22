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

        private void Awake()
        {
            _itemSlotController.OnItemSlotSelection += UpdateItemList;
            _templateController.OnBuildingAdded += UpdateGrid;
        }

        private void OnDisable()
        {
            _itemSlotController.OnItemSlotSelection -= UpdateItemList;
            _templateController.OnBuildingAdded -= UpdateGrid;
        }

        void UpdateItemList(List<Equipment> equipmentList)
        {
            _itemList = equipmentList;
            UpdateGrid();
        }

    }
}