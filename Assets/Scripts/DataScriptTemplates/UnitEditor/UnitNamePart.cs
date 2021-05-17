using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitName", menuName = "Unit Editor/Name Component", order = 0)]
public class UnitNamePart : ScriptableObject
{
    public List<string> prefix;
    public List<string> main;
    public List<string> suffix;
}
