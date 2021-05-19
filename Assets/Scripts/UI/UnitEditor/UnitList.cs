using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitList : UIPanelWithGrid<UnitTemplate>
    {
        [SerializeField] Image _classSelector;
        [SerializeField] UnitsListController _unitsListController;

        void Awake()
        {
            FillLayoutElementsList();
        }

        void OnEnable()
        {
            UpdateGrid();
        }

        protected override void FillLayoutElementsList()
        {
            _layoutElementsData = _unitsListController.currentTemplates;
        }

        protected override void PlusButtonListener()
        {
            _classSelector.gameObject.SetActive(true);
        }
    }

}