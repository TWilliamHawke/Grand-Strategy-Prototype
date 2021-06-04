using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectorSettlement : BuildingSelectionPanel
{
    Settlement _settlement;

    private void OnEnable() {
        Settlement.OnBuildingConstructed += UpdateSelector;
    }

    private void OnDisable() {
        Settlement.OnBuildingConstructed -= UpdateSelector;
    }

    public void SetSettlementData(Settlement data)
    {
        _settlement = data;
        BuildingSelectionButtonSettlement.settlement = data;
    }

    protected override List<Building> GetWasteBuildings()
    {
        return _settlement.constructedBuildings;
    }
}
