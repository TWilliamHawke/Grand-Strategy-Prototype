using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TechName", menuName = "Other/Technology")]
public class Technology : ScriptableObject
{
    public string localizedName;

    public List<Effect> effects = new List<Effect>();

}
