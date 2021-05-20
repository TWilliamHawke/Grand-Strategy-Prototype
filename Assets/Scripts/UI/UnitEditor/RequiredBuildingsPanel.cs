using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class RequiredBuildingsPanel : UIPanelWithGrid<Building>
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] BuildingSelector _buildingSelector;

        private void OnEnable() {
            FillLayoutElementsList();
            UpdateGrid();
        }

        void Awake()
        {
            _templateController.OnBuildingAdded += FillAndUpdate;
            _templateController.OnTemplateSelection += FillAndUpdate;
        }

        void OnDestroy()
        {
            _templateController.OnBuildingAdded -= FillAndUpdate;
            _templateController.OnTemplateSelection -= FillAndUpdate;
        }

        void FillAndUpdate()
        {
            FillLayoutElementsList();
            UpdateGrid();
        }

        protected override void FillLayoutElementsList()
        {
            _layoutElementsData = _templateController.currentTemplate.requiredBuildings;
        }

        protected override void PlusButtonListener()
        {
            _buildingSelector.Show();
        }
    }
}