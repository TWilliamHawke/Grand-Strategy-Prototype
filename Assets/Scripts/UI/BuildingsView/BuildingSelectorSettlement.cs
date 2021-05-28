using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectorSettlement : BuildingSelectionPanel
{
    SettlementData _settlementData;

    private void OnEnable() {
        SettlementData.OnBuildingConstructed += UpdateSelector;
    }

    private void OnDisable() {
        SettlementData.OnBuildingConstructed -= UpdateSelector;
    }

    public void SetSettlementData(SettlementData data)
    {
        _settlementData = data;
        BuildingSelectionButtonSettlement.settlementData = data;
    }

    protected override List<Building> GetWasteBuildings()
    {
        return _settlementData.constructedBuildings;
    }
}
