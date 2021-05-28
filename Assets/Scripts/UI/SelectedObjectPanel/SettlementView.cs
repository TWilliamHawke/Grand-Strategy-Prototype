using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;
using UnityEngine.UI;

public class SettlementView : UIScreen, INeedInit
{
    [SerializeField] Text _settlementName;
    [SerializeField] GarrisonView _garrisonView;
    [SerializeField] BuildingsView _buildingsView;

    void OnDestroy()
    {
        Settlement.OnSettlementSelect -= UpdateSettlementInfo;
        SelectionController.OnSelectionCancel -= Close;
        SelectionController.OnSelect -= CheckSelectedTarget;
    }

    public void Init()
    {
        Settlement.OnSettlementSelect += UpdateSettlementInfo;
        SelectionController.OnSelectionCancel += Close;
        SelectionController.OnSelect += CheckSelectedTarget;
    }

    void UpdateSettlementInfo(SettlementData settlementData)
    {
        _settlementName.text = settlementData.localizedName;
        UnitSelectionButton.selectedSettlement = settlementData;
        _garrisonView.UpdateUnitsCards(settlementData);
        _buildingsView.UpdateConstructedBuildings(settlementData);
        Show();
    }

    void CheckSelectedTarget(ISelectable target)
    {
        if (target is Settlement) return;

        Close();
    }

}
