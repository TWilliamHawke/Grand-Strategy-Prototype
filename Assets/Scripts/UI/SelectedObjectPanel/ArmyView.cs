using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyView : UIScreen, INeedInit
{
    [SerializeField] UnitListPanel _unitsView;
    [SerializeField] Text _armyName;

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
        _armyName.text = army.localizedName;
        Show();
    }

}
