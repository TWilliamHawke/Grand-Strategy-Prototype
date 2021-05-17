using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "ArmorInfo", menuName = "Unit Editor/Armor", order = 50)]
public class ArmourInfo : Equipment
{
    public int defence;
    [Range(0, 1)]
    public float speedMult = 1;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(Name);
        stringBuilder.AppendLine("<i>Armor</i>");
        stringBuilder.Append("Defence: ").Append(defence).AppendLine();
        stringBuilder.Append("Speed: ").Append(speedMult*100).Append("%");

        return stringBuilder.ToString();
    }
}
