using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitClass", menuName = "Unit Editor/UnitClass", order = 1)]
public class UnitClass : ScriptableObject
{
    public string className;
    public UnitNamePart possibleNames;
    public int wealth;
    public int weaponSkill;
    public int unitSize;
    public int health;
    public int speed = 40;
    public int morale;
    public Sprite defaultIcon;
    public Sprite unitPreview;

    private void OnEnable() {
        if(possibleNames == null)
        {
            Debug.LogError($"Unit class {name} doesn't have any unit names!", this);
        }

    }
}
