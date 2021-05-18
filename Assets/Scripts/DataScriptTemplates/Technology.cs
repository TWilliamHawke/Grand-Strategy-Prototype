using System.Collections;
using System.Collections.Generic;
using System.Text;
using Effects;
using UnityEngine;

[CreateAssetMenu(fileName = "TechName", menuName = "Core Game/Technology")]
public class Technology : ScriptableObject
{
    public string localizedName;

    public List<Effect> effects = new List<Effect>();

    public string GetEffectsDescription()
    {
        var stringBuilder = new StringBuilder();
        int i = 0;

        foreach(var effect in effects)
        {
            i++;
            if (i == effects.Count)
            {
                stringBuilder.Append(effect.GetText());
            }
            else
            {
                stringBuilder.AppendLine(effect.GetText());
            }
        }

        return stringBuilder.ToString();
    }

}
