using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "ArmorInfo", menuName = "Unit Editor/Armor", order = 50)]
public class ArmourInfo : Equipment
{
    [Header("Armor data")]
    public int defence;
    [Range(0, 1)]
    public float speedMult = 1;

    public override string GetToolTipText()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(Name)
            .AppendLine("<i>Armor</i>")
            .Append("Defence: ").AppendLine(defence.ToString())
            .Append("Speed: ").Append(speedMult*100).Append("%");

        return stringBuilder.ToString();
    }
}
