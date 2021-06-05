using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Unit Editor/Shield", order = 53)]
public class Shield : RequireSkillEquipment
{
    [Header("Shield data")]
    [Range(0, 1)] public float missileBlockChance;
    public int defence;
    public bool isPavese;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(Name).AppendLine();
        stringBuilder.Append("<i>Shield</i>").AppendLine();
        stringBuilder.Append("Melee defence: ").Append(defence).AppendLine();
        stringBuilder.Append("Missile resist: ").Append(missileBlockChance*100).Append("%");


        return stringBuilder.ToString();
    }

}
