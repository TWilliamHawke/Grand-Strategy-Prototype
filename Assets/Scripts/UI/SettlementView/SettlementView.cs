using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementView : UIScreen
{
    [SerializeField] Text _settlementName;
    [SerializeField] GarrisonView _garrisonView;

    private void Awake()
    {
        Settlement.OnSettlementSelect += UpdateSettlementInfo;
        SelectionController.OnSelectionCancel += Close;
        SelectionController.OnSelect += CheckSelectedTarget;
        Close();
    }

    private void OnDestroy()
    {
        Settlement.OnSettlementSelect -= UpdateSettlementInfo;
        SelectionController.OnSelectionCancel -= Close;
        SelectionController.OnSelect -= CheckSelectedTarget;
    }

    void UpdateSettlementInfo(Settlement settlement)
    {
        Show();
        _settlementName.text = settlement.GetName();
        _garrisonView.UpdateUnitsCards(settlement);
    }

    void CheckSelectedTarget(ISelectable target)
    {
        if (target is Settlement) return;

        Close();
    }
}
