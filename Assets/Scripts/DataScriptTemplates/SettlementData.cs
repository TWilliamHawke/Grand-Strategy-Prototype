using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "settlement", menuName = "Core Game/Settlement data")]
public class SettlementData : ScriptableObject, IHaveUnits, IBuildController
{
    [SerializeField] string _name = "test";
    [SerializeField] Faction _owner;

    [SerializeField] List<Building> _startBuildings = new List<Building>();
    [SerializeField] List<UnitTemplate> _startGarrison = new List<UnitTemplate>();

    List<Unit> _garrison = new List<Unit>();
    List<Building> _constructedBuildings = new List<Building>();
    Vector3 _position = Vector3.zero;

    public List<Unit> unitsFromThisSettelment { get; } = new List<Unit>();

    //getters
    public string localizedName => _name;
    public List<Building> constructedBuildings => _constructedBuildings;
    public List<Unit> unitList => _garrison;
    public Vector3 position => _position;
    public bool isPlayerSettlement => _owner.isPlayerFaction;
    //TODO find solution without putting UnitListController into every settlementData
    public bool forceIsFull => unitList.Count >= 7;

    //events
    public static event UnityAction OnBuildingConstructed;
    public static event UnityAction<UnitTemplate> OnUnitAdded;


    private void OnEnable()
    {
        SetStartGarrison();
        SetStartBuildings();
    }

    public void SetSettlementPostion(Vector3 settlementPosition)
    {
        _position = settlementPosition;
    }

    public void RemoveUnit(Unit unit)
    {
        _garrison.Remove(unit);
    }

    public void AddBuilding(Building building)
    {
        _constructedBuildings.Add(building);
        OnBuildingConstructed?.Invoke();
    }

    void SetStartGarrison()
    {
        _garrison.Clear();
        foreach (var template in _startGarrison)
        {
            AddUnitToGarrison(template);
        }
    }

    void SetStartBuildings()
    {
        _constructedBuildings.Clear();
        _constructedBuildings.AddRange(_startBuildings);
    }

    public void AddUnit(UnitTemplate template)
    {
        AddUnitToGarrison(template);
        OnUnitAdded?.Invoke(template);
    }

    private void AddUnitToGarrison(UnitTemplate template)
    {
        var unit = new Unit(template, this);
        _garrison.Add(unit);
        unitsFromThisSettelment.Add(unit);
    }
}
