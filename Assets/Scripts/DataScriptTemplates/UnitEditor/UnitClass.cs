using System.Collections;
using System.Collections.Generic;
using Effects;
using UnitEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitClass", menuName = "Unit Editor/UnitClass", order = 1)]
public class UnitClass : ScriptableObject
{
    public string className;
    public UnitNamePart possibleNames;
    public int wealth;
    [TextArea(4, 5)]
    [SerializeField] string _description;

    [Header("Visualization")]
    [SpritePreview(64, 64)]
    public Sprite defaultIcon;
    [SpritePreview(64, 64)]
    public Sprite unitPreview;

    [Header("Battle Stats")]
    public int weaponSkill;
    public int unitSize;
    public int health;
    public int speed = 40;
    public int morale;

    [Space(5)]
    public List<Building> requiredBuildings;

    public string description => _description;

    private void OnEnable()
    {
        if (possibleNames == null)
        {
            Debug.LogError($"Unit class {name} doesn't have any unit names!", this);
        }
    }

}
