using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Mount", menuName = "Unit Editor/Mount", order = 55)]
public class Mount : Equipment
{
    public int defence;
    public int speed;
    public int health;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(Name).AppendLine();
        stringBuilder.AppendLine("<i>Mount</i>");
        stringBuilder.Append("Armor: ").Append(defence).AppendLine();
        stringBuilder.Append("Speed: ").Append(speed).AppendLine();
        stringBuilder.Append("Health: ").Append(health);


        return stringBuilder.ToString();
    }

}
