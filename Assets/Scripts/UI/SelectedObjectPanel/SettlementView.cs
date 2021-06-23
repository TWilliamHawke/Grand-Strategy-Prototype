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
    [SerializeField] GlobalMapSelectable _selector;

    void OnDestroy()
    {
        Settlement.OnSettlementSelect -= UpdateSettlementInfo;
        _selector.OnSelectionCancel -= Close;
        _selector.OnSelect -= CheckSelectedTarget;
    }

    public void Init()
    {
        Settlement.OnSettlementSelect += UpdateSettlementInfo;
        _selector.OnSelectionCancel += Close;
        _selector.OnSelect += CheckSelectedTarget;
    }

    void UpdateSettlementInfo(Settlement settlementData)
    {
        _settlementName.text = settlementData.localizedName;
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
