using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnitEditor;
using Effects;

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "Unit Editor/Unit Template", order = 3)]
public class UnitTemplate : ScriptableObject
{
    public string templateName;
    public UnitClass unitClass;
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public ArmourInfo armour;
    public Shield shield;
    public Mount mount;
    public bool canNotEdit;


    public int health => mount?.health != 0 ? mount.health : unitClass.health;
    public int speed => GetSpeed();
    public int damage => primaryWeapon.damage;
    public int defence => armour.defence;
    public int attack => unitClass.weaponSkill;
    public bool isRange => primaryWeapon is RangeWeapon || secondaryWeapon is RangeWeapon;
    public int charge => (primaryWeapon as MeleeWeapon)?.charge ?? 0;
    public List<Building> requiredBuildings { get; set; } = new List<Building>();

    private void OnEnable() {
        requiredBuildings.Clear();
        requiredBuildings.AddRange(unitClass.requiredBuildings);
    }

    public int GetEquipmentTotalCost()
    {
        var totalcost = primaryWeapon.goldCost
                        + secondaryWeapon.goldCost
                        + armour.goldCost
                        + shield.goldCost
                        + mount.goldCost;
        return totalcost;
    }

    int GetSpeed()
    {
        if(mount?.speed != 0) return mount.speed;

        float dismountedSpeed = unitClass.speed * armour.speedMult;
        return Mathf.CeilToInt(dismountedSpeed);
    }

    public List<UnitNamePart> GetTypeNames()
    {
        var names = new List<UnitNamePart>();
        names.Add(unitClass.possibleNames);
        names.Add(mount.unitNames);

        return names;
    }

    public List<UnitNamePart> GetEquipmentNames()
    {
        var names = new List<UnitNamePart>();
        names.Add(armour.unitNames);
        names.Add(shield.unitNames);
        names.Add(primaryWeapon.unitNames);
        if(primaryWeapon.unitNames != secondaryWeapon.unitNames)
        {
            names.Add(secondaryWeapon.unitNames);
        }

        return names;
    }

    public List<Effect> FindBuildingsEffects()
    {
        var effects = new List<Effect>();
        foreach(var bulding in requiredBuildings)
        {
            effects.AddRange(bulding.effects);
        }

        return effects;
    }
    
}
