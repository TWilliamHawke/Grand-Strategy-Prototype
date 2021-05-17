using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitEditor;

[CreateAssetMenu(fileName = "IncreaseClassWealth", menuName = "Effects/IncreaseClassWealth", order = 20)]
public class IncreaseClassWealth : Effect
{
    [SerializeField] UnitClass _unitClass;
    [SerializeField] int addWealth;


    public override string GetText()
    {
        return $"Increases wealth for {_unitClass.className} by {addWealth}";
    }
}
