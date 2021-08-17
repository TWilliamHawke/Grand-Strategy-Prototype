using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitEditor;

public class UnitStats : MonoBehaviour, IStats
{
    UnitTemplate _unitTemplate;

    //getters
    public int health => _unitTemplate.mount?.health != 0 ? _unitTemplate.mount.health : _unitTemplate.unitClass.health;
    public int speed => GetSpeed();
    public int damage => _unitTemplate.primaryWeapon.damage;
    public int defence => 5 + _unitTemplate.meleeSkill + _unitTemplate.armour.defence;
    public int attack => 5 + _unitTemplate.meleeSkill;
    public int charge => (_unitTemplate.primaryWeapon as MeleeWeapon)?.charge ?? 0;
    public bool isRange => _unitTemplate.primaryWeapon is RangeWeapon || _unitTemplate.secondaryWeapon is RangeWeapon;


    public UnitStats(UnitTemplate unitTemplate)
    {
        _unitTemplate = unitTemplate;
    }

    int GetSpeed()
    {
        if (_unitTemplate.mount?.speed != 0) return _unitTemplate.mount.speed;

        float dismountedSpeed = _unitTemplate.unitClass.speed * _unitTemplate.armour.speedMult;
        return Mathf.CeilToInt(dismountedSpeed);
    }


}
