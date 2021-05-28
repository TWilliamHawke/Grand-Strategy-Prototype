using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitEditor
{
    public class RequiredBuildingsPanelPlus : UIPanelWithGridPlus<Building>
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] BuildingsListController _buildingsListController;
        [SerializeField] BuildingSelectionPanel _buildingSelector;

        List<Building> _requiredBuildings = new List<Building>();

        protected override List<Building> _layoutElementsData => _requiredBuildings;

        private void OnEnable()
        {
            FillAndUpdate();
        }

        void OnDisable()
        {
            _buildingSelector.Close();
        }

        void Awake()
        {
            _templateController.OnBuildingAdded += FillAndUpdate;
        }

        void OnDestroy()
        {
            _templateController.OnBuildingAdded -= FillAndUpdate;
        }

        protected override void PlusButtonListener()
        {
            _buildingSelector.Show();
        }

        protected override bool ShouldHidePlusButton()
        {
            var possibleBuildings = _buildingsListController.FilterBuildings(BuildingSlots.castleAny);
            int possibleBuildinsCount = possibleBuildings.Count;

            foreach (var building in possibleBuildings)
            {
                if (_requiredBuildings.Contains(building))
                {
                    possibleBuildinsCount--;
                }
            }

            return possibleBuildinsCount <= 0;
        }

        void FillAndUpdate()
        {
            _requiredBuildings = _templateController.currentTemplate.requiredBuildings;
            UpdateGrid();
        }

    }
}