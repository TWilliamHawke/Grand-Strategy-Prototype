using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnitEditor;
using Effects;
using System;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "Unit Editor/Unit Template", order = 3)]
public class UnitTemplate : ScriptableObject
{
    public string templateName => GetFullName();

    [Header("Name Parts")]
    public string namePrefix;
    public string nameCore;
    public string nameSuffix;
    [Space(10)]
    public UnitClass unitClass;
    [Header("Equipment")]
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

    public List<Building> requiredBuildings => GetRequiredBuildings();
    List<Building> _requiredBuildings = new List<Building>();

    public List<UnitNamePart> GetPossibleNamesByType()
    {
        var names = new List<UnitNamePart>();
        names.Add(unitClass.possibleNames);
        names.Add(mount.unitNames);

        return names;
    }

    public List<UnitNamePart> GetPossibleNamesByEquipment()
    {
        var names = new List<UnitNamePart>();
        names.Add(armour.unitNames);
        names.Add(shield.unitNames);
        names.Add(primaryWeapon.unitNames);
        if (primaryWeapon.unitNames != secondaryWeapon.unitNames)
        {
            names.Add(secondaryWeapon.unitNames);
        }

        return names;
    }

    public UnitTemplate Clone()
    {
        var instance = Instantiate(this);
        //after Instantiation object loses all required buldings
        //so they should be copy from current object
        instance.ReplaceReqRequiredBuildings(_requiredBuildings);

        return instance;
    }

    public void ReplaceReqRequiredBuildings(List<Building> buildings)
    {
        _requiredBuildings.Clear();
        foreach(var building in buildings)
        {
            _requiredBuildings.Add(building);
        }
    }

    public void AddRequiredBuildings(Building building)
    {
        _requiredBuildings.Add(building);
    }

    public List<Effect> FindBuildingsEffects()
    {
        return requiredBuildings.SelectMany(t => t.effects).ToList();;
    }

    List<Building> GetRequiredBuildings()
    {
        var list = new List<Building>();

        list.AddRange(unitClass.requiredBuildings);
        list.AddRange(_requiredBuildings);
        return list;
    }

    int GetSpeed()
    {
        if (mount?.speed != 0) return mount.speed;

        float dismountedSpeed = unitClass.speed * armour.speedMult;
        return Mathf.CeilToInt(dismountedSpeed);
    }

    string GetFullName()
    {
        var name = $"{namePrefix} {nameCore} {nameSuffix}".Trim();
        var capitalizedName = name.First().ToString().ToUpper() + name.Substring(1);

        var regex = new Regex(@" -");

        return regex.Replace(capitalizedName, "-");
    }
}
