using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour, IhaveLabel, ISelectable, IHaveUnits
{
    //TODO here will be scriptableobject with settlementinfo
    //instead of just name
    [SerializeField] string _name = "test";
    [SerializeField] List<UnitTemplate> startGarrison;
    [SerializeField] MeshRenderer _selector;

    public List<Unit> unitList { get; set; } = new List<Unit>();

    public static event System.Action<Settlement> OnSettlementInit;
    public static event System.Action<Settlement> OnSettlementSelect;



    public void Deselect()
    {
        _selector.gameObject.SetActive(false);
    }

    public string GetName() => _name;

    public void Select()
    {
        _selector.gameObject.SetActive(true);
        OnSettlementSelect?.Invoke(this);
    }

    void Start()
    {
        SetStartGarrison();
        OnSettlementInit?.Invoke(this);
    }

    void SetStartGarrison()
    {
        foreach(var template in startGarrison)
        {
            var unit = new Unit(template, this);
            unitList.Add(unit);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        if(unitList.Contains(unit))
        {
            unitList.Remove(unit);
        }
    }
}