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
            _layoutElementsData = equipmentList;
            UpdateGrid();
        }

        protected override void FillLayoutElementsList()
        {
        }

        protected override void PlusButtonListener()
        {
        }

    }
}