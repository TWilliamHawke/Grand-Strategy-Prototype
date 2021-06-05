using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Weapon : RequireSkillEquipment
{
    [Header("Weapon Stats")]
    public int damage;
    public int armorPiercing;
    public bool twoHanded;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        string weaponType = twoHanded ? "<i>Two handed weapon</i>" : "<i>One handed weapon</i>";
        stringBuilder.AppendLine(Name);
        stringBuilder.AppendLine(weaponType);
        stringBuilder.Append("Damage: ").Append(damage.ToString());

        return stringBuilder.ToString();
    }

}
