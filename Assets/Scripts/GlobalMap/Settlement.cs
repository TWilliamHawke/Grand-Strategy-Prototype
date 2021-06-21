using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Settlement : MonoBehaviour, IhaveLabel, ISelectable, IHaveUnits, IBuilder
{
    //TODO replace this by selectionController
    static public Settlement selectedSettlement => _selectedSettlement;
    static Settlement _selectedSettlement;

    [SerializeField] SettlementData _settlementData;
    [SerializeField] MeshRenderer _selector;

    //dynamic data
    List<Building> _constructedBuildings = new List<Building>();
    List<Unit> _unitsFromThisSettelment = new List<Unit>();
    List<Unit> _garrison = new List<Unit>();
    Faction _owner;

    //getters
    public List<Building> constructedBuildings => _constructedBuildings;
    public List<Unit> unitsFromThisSettelment => _unitsFromThisSettelment;
    public string localizedName => _settlementData.localizedName;
    public List<Unit> unitList => _garrison;
    public Color baseLabelColor => _owner.factionColor;
    public bool isPlayerSettlement => _owner.isPlayerFaction;

    //event
    public static event UnityAction<Settlement> OnSettlementInit;
    public static event UnityAction<Settlement> OnSettlementSelect;
    public static event UnityAction<Settlement> OnConquest;
    public static event UnityAction<UnitTemplate> OnUnitAdded;
    public static event UnityAction OnBuildingConstructed;

    void Awake()
    {
        _owner = _settlementData.originalOwner;
        SetStartBuildings();
        SetStartGarrison();
    }

    void Start()
    {
        //called after all event subscription
        OnSettlementInit?.Invoke(this);
    }


    public void Deselect()
    {
        _selector.gameObject.SetActive(false);
    }

    public string GetName() => _settlementData.localizedName;

    public void Select()
    {
        _selector.gameObject.SetActive(true);
        OnSettlementSelect?.Invoke(this);
        _selectedSettlement = this;
    }


    public void AddBuilding(Building building)
    {
        _constructedBuildings.Add(building);
        OnBuildingConstructed?.Invoke();
    }

    public void RemoveUnit(Unit unit)
    {
        _garrison.Remove(unit);
    }

    public void AddUnit(UnitTemplate template)
    {
        AddUnitToGarrison(template);
        OnUnitAdded?.Invoke(template);
    }

    public void Defeat(Faction winnerFaction)
    {
        _garrison.Clear();
        _owner = winnerFaction;
        OnConquest?.Invoke(this);
    }

    void AddUnitToGarrison(UnitTemplate template)
    {
        var unit = new Unit(template, this);
        _garrison.Add(unit);
        _unitsFromThisSettelment.Add(unit);
    }

    void SetStartGarrison()
    {
        _garrison.Clear();
        foreach (var template in _settlementData.startGarrison)
        {
            AddUnitToGarrison(template);
        }
    }

    void SetStartBuildings()
    {
        _constructedBuildings.Clear();
        _constructedBuildings.AddRange(_settlementData.startBuildings);
    }

}