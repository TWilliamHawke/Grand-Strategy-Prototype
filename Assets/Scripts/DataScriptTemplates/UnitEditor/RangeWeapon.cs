using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "Unit Editor/Range Weapon", order = 52)]
public class RangeWeapon : Weapon
{
    public int range;
    public int ammo;
    public int accuracy;
    public bool allowFireShot;
    public bool allowPavese;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(Name).AppendLine();
        stringBuilder.Append("<i>Range weapon</i>").AppendLine();
        stringBuilder.Append("Damage: ").Append(damage).AppendLine();
        stringBuilder.Append("Range: ").Append(range).AppendLine();
        stringBuilder.Append("Ammo: ").Append(ammo);

        return stringBuilder.ToString();
    }

}
