using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Unit Editor/Melee Weapon", order = 51)]
public class MeleeWeapon : Weapon
{
    public int charge;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(base.GetToolTipText());
        stringBuilder.Append("Charge: ").Append(charge);

        return stringBuilder.ToString();
    }
}
