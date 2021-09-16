using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;

public class UnitStats : IStats
{
    UnitTemplate _unitTemplate;
    UnitClass _unitClass;
    MeleeWeapon _meleeWeapon;
    RangeWeapon _rangeWeapon;
    Shield _shield;
    ArmourInfo _armor;
    Mount _mount;


    //getters
    public int health => _mount && _mount.health != 0 ? _mount.health : _unitClass.health;
    public int speed => GetSpeed();
    public int damage => _meleeWeapon?.damage ?? 0;
    public int defence => 5 + _unitTemplate.meleeSkill + _armor.defence;
    public int attack => 5 + _unitTemplate.meleeSkill;
    public int charge => _meleeWeapon?.charge ?? 0;


    public UnitStats(UnitTemplate unitTemplate)
    {
        _unitClass = unitTemplate.unitClass;
        _unitTemplate = unitTemplate;

        var primaryWeapon = unitTemplate.inventory[EquipmentSlots.primaryWeapon];
        var secondaryWeapon = unitTemplate.inventory[EquipmentSlots.secondaryWeapon];

        _meleeWeapon = primaryWeapon as MeleeWeapon ?? secondaryWeapon as MeleeWeapon;
        _rangeWeapon = primaryWeapon as RangeWeapon ?? secondaryWeapon as RangeWeapon;
        _armor = unitTemplate.inventory[EquipmentSlots.armour] as ArmourInfo;
        _shield = unitTemplate.inventory[EquipmentSlots.shield] as Shield;
        _mount = unitTemplate.inventory[EquipmentSlots.mount] as Mount;

    }

    int GetSpeed()
    {
        if (_mount && _mount.speed != 0) return _mount.speed;

        float dismountedSpeed = _unitClass.speed * _armor.speedMult;
        return Mathf.CeilToInt(dismountedSpeed);
    }


}
