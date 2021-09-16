using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Effects;
using UnitEditor;

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "Unit Editor/Unit Template", order = 3)]
public class UnitTemplate : ScriptableObject, ITemplate
{
    public string fullName => _nameParts.GetFullName();

    [SerializeField] PartialName _nameParts;
    [Space(10)]
    public UnitClass unitClass;
    [SerializeField] bool _canNotEdit;
    [SerializeField] int _meleeSkill;

    [SerializeField] Inventory _inventory = new Inventory();
    List<Building> _requiredBuildings = new List<Building>();

    //getters
    public PartialName nameParts => _nameParts;
    public bool canNotEdit => _canNotEdit;

    public List<Building> requiredBuildings => unitClass.requiredBuildings.Concat(_requiredBuildings).ToList();
    
    public int rangeSkill => unitClass.weaponSkill - _meleeSkill;
    public int meleeSkill
    {
        get => _meleeSkill;
        set => _meleeSkill = value;
    }

    public Inventory inventory => _inventory;

    public List<UnitNamePart> GetPossibleNamesByType()
    {
        var names = new List<UnitNamePart>();
        names.Add(unitClass.possibleNames);
        names.Add(_inventory[EquipmentSlots.mount]?.unitNames);

        return names;
    }

    public List<UnitNamePart> GetPossibleNamesByEquipment()
    {
        var names = new List<UnitNamePart>();
        names.Add(_inventory[EquipmentSlots.armour]?.unitNames);
        names.Add(_inventory[EquipmentSlots.shield]?.unitNames);

        var primaryWeaponNames = _inventory[EquipmentSlots.primaryWeapon]?.unitNames;
        var secondaryWeaponNames = _inventory[EquipmentSlots.secondaryWeapon]?.unitNames;

        names.Add(primaryWeaponNames);
        if (primaryWeaponNames != secondaryWeaponNames)
        {
            names.Add(secondaryWeaponNames);
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

    void ReplaceRequiredBuildings(List<Building> buildings)
    {
        _requiredBuildings.Clear();
        _requiredBuildings.AddRange(buildings);
    }

    public bool FindEquipment<T>(EquipmentSlots slot, out T equipment) where T : Equipment
    {
        equipment = _inventory[slot] as T;

        return equipment == null ? false : true;
    }

    public void AddEquipment(EquipmentSlots slot, Equipment equipment)
    {
        _inventory[slot] = equipment;
    }
}
