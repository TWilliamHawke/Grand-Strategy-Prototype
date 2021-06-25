using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Effects;
using System.Text.RegularExpressions;

public interface ITemplate : IRequireBuildings
{
    string fullName { get; }
    bool canNotEdit { get; }
    List<UnitNamePart> GetPossibleNamesByEquipment();
    List<UnitNamePart> GetPossibleNamesByType();
    void Save(UnitsListController unitsListController);
}

public interface IPrototype<T> where T : class
{
    T Clone();
}

public interface IRequireBuildings
{
    List<Building> requiredBuildings { get; }

    void AddRequiredBuildings(Building building);
    bool TryRemoveRequiredBuiding(Building building);
    List<Effect> FindBuildingsEffects();
}

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "Unit Editor/Unit Template", order = 3)]
public class UnitTemplate : ScriptableObject, ITemplate
{
    public string fullName => _nameParts.GetFullName();

    [SerializeField] PartialName _nameParts;
    [Space(10)]
    public UnitClass unitClass;
    [Header("Equipment")]
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public ArmourInfo armour;
    public Shield shield;
    public Mount mount;
    [SerializeField] bool _canNotEdit;

    List<Building> _requiredBuildings = new List<Building>();

    //getters
    public PartialName nameParts => _nameParts;
    public int health => mount?.health != 0 ? mount.health : unitClass.health;
    public int speed => GetSpeed();
    public int damage => primaryWeapon.damage;
    public int defence => armour.defence;
    public int attack => unitClass.weaponSkill;
    public int charge => (primaryWeapon as MeleeWeapon)?.charge ?? 0;
    public bool isRange => primaryWeapon is RangeWeapon || secondaryWeapon is RangeWeapon;
    public bool canNotEdit => _canNotEdit;

    public List<Building> requiredBuildings => unitClass.requiredBuildings.Concat(_requiredBuildings).ToList();

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
        instance.ReplaceRequiredBuildings(_requiredBuildings);

        return instance;
    }

    public void AddRequiredBuildings(Building building)
    {
        _requiredBuildings.Add(building);
    }

    public bool TryRemoveRequiredBuiding(Building building)
    {
        return _requiredBuildings.Remove(building);
    }

    public List<Effect> FindBuildingsEffects()
    {
        return requiredBuildings.SelectMany(t => t.effects).ToList();
    }

    public void SetNames(PartialName names)
    {
        _nameParts = names;
    }

    public void AllowEdit()
    {
        _canNotEdit = false;
    }

    public void Save(UnitsListController unitsListController)
    {
        unitsListController.SaveUnitTemplate(this);
    }

    int GetSpeed()
    {
        if (mount?.speed != 0) return mount.speed;

        float dismountedSpeed = unitClass.speed * armour.speedMult;
        return Mathf.CeilToInt(dismountedSpeed);
    }

    void ReplaceRequiredBuildings(List<Building> buildings)
    {
        _requiredBuildings.Clear();
        _requiredBuildings.AddRange(buildings);
    }

}
