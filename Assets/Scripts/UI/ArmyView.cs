using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyView : UIScreen
{
    [SerializeField] UnitsView _unitsView;

    void Awake()
    {
        Army.OnArmySelected += UpdateArmyInfo;
        Army.OnArmyDeselected += Close;
        Close();
    }

    void OnDestroy()
    {
        Army.OnArmySelected -= UpdateArmyInfo;
        Army.OnArmyDeselected -= Close;
    }

    void UpdateArmyInfo(Army army)
    {
        _unitsView.UpdateUnitsCards(army);
        Show();
    }

}
