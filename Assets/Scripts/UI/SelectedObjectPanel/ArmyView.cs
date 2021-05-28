using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyView : UIScreen, INeedInit
{
    [SerializeField] UnitListPanel _unitsView;

    void OnDestroy()
    {
        Army.OnArmySelected -= UpdateArmyInfo;
        Army.OnArmyDeselected -= Close;
    }

    public void Init()
    {
        Army.OnArmySelected += UpdateArmyInfo;
        Army.OnArmyDeselected += Close;
    }

    void UpdateArmyInfo(Army army)
    {
        _unitsView.UpdateUnitsCards(army);
        Show();
    }

}
