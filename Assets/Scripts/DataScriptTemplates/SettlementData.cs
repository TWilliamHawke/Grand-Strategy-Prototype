using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "settlement", menuName = "Core Game/Settlement data")]
public class SettlementData : ScriptableObject
{
    [SerializeField] string _name = "test";
    [SerializeField] FactionData _originalOwner;
    [SerializeField] List<Building> _startBuildings = new List<Building>();
    [SerializeField] List<UnitTemplate> _startGarrison = new List<UnitTemplate>();

    //getters
    public string localizedName => _name;
    public List<Building> startBuildings => _startBuildings;
    public FactionData originalOwner => _originalOwner;
    public List<UnitTemplate> startGarrison => _startGarrison;

}
