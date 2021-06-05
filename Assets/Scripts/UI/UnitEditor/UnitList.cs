using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitList : UIPanelWithGridPlus<UnitTemplate>
    {
        [SerializeField] Image _classSelector;
        [SerializeField] UnitsListController _unitsListController;

        protected override List<UnitTemplate> _layoutElementsData => _unitsListController.currentTemplates;

        void OnEnable()
        {
            UpdateLayout();
        }

        protected override void PlusButtonListener()
        {
            _classSelector.gameObject.SetActive(true);
        }

        protected override bool ShouldHidePlusButton()
        {
            return false;
        }
    }

}