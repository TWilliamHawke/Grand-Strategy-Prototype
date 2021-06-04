using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsView : UIPanelWithGridPlus<Building>
{
    [SerializeField] BuildingSelectorSettlement _buildingSelector;
    [SerializeField] BuildingsListController _buildingsListController;

    protected override List<Building> _layoutElementsData => _constructedBuildings;

    List<Building> _constructedBuildings = new List<Building>();
    Settlement _settlement;

    private void OnEnable()
    {
        Settlement.OnBuildingConstructed += UpdateGrid;
    }

    private void OnDisable()
    {
        Settlement.OnBuildingConstructed -= UpdateGrid;
        _buildingSelector.Close();
    }

    protected override void PlusButtonListener()
    {
        _buildingSelector.SetSettlementData(_settlement);
        _buildingSelector.Show();
    }

    protected override bool ShouldHidePlusButton()
    {
        //cannot build in ai settlemrnt
        if (!_settlement.isPlayerSettlement) return true;

        var possibleBuildings = _buildingsListController.FilterBuildings(BuildingSlots.castleAny);
        int possibleBuildinsCount = possibleBuildings.Count;

        foreach (var building in possibleBuildings)
        {
            if (_constructedBuildings.Contains(building))
            {
                possibleBuildinsCount--;
            }
        }

        return possibleBuildinsCount <= 0;
    }

    public void UpdateConstructedBuildings(Settlement settlement)
    {
        _settlement = settlement;
        _constructedBuildings = settlement.constructedBuildings;
        UpdateGrid();
    }
}
