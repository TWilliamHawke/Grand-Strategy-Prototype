using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{

    public class ItemList : UIPanelWithGrid<Equipment>
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;

        protected override List<Equipment> _layoutElementsData => _itemList;

        List<Equipment> _itemList = new List<Equipment>();

        void OnEnable()
        {
            ClearLayout();
            _itemSlotController.OnItemSlotSelection += UpdateItemList;
            _templateController.OnBuildingsChange += UpdateLayout;
            _itemSlotController.OnItemReset += UpdateLayout;
            _templateController.OnWeaponSkillChange += UpdateLayout;
        }

        void OnDisable()
        {
            _itemSlotController.OnItemSlotSelection -= UpdateItemList;
            _templateController.OnBuildingsChange -= UpdateLayout;
            _itemSlotController.OnItemReset -= UpdateLayout;
            _templateController.OnWeaponSkillChange -= UpdateLayout;
        }

        void UpdateItemList(List<Equipment> equipmentList)
        {
            _itemList = equipmentList;
            UpdateLayout();
        }

    }
}