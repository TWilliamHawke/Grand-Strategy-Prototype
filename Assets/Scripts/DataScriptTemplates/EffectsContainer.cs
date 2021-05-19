using System.Collections.Generic;
using System.Text;
using Effects;
using UnityEngine;

abstract public class EffectsContainer : ScriptableObject
{
    abstract public List<Effect> effects { get; }  //for properly order in inspector

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
