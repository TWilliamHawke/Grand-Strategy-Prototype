using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class RequiredBuildingsPanel : UIPanelWithGrid<Building>
    {
        [SerializeField] TemplateController _templateController;

        List<Building> _requiredBuildings = new List<Building>();

        protected override List<Building> _layoutElementsData => _requiredBuildings;

        private void OnEnable()
        {
            FillAndUpdate();
        }

        void Awake()
        {
            _templateController.OnTemplateSelection += FillAndUpdate;
        }

        void OnDestroy()
        {
            _templateController.OnTemplateSelection -= FillAndUpdate;
        }

        void FillAndUpdate()
        {
            _requiredBuildings = _templateController.currentTemplate.requiredBuildings;
            UpdateLayout();
        }

    }
}