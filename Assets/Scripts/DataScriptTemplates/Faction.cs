using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TechName", menuName = "Core Game/Faction")]
public class Faction : ScriptableObject
{
    [SerializeField] string _factionName = " EmptyName";
    [SerializeField] Color _factionColor = Color.white;
    [SerializeField] bool _isPlayerFaction = false;

    public string factionName => _factionName;
    public Color factionColor => _factionColor;
    public bool isPlayerFaction => _isPlayerFaction;


    List<Technology> _researchedTechnology = new List<Technology>();
    List<Technology> researchedTechnology => _researchedTechnology;

    void OnEnable() {
        _researchedTechnology.Clear();
    }

    public void AddTechnology(Technology tech)
    {
        _researchedTechnology.Add(tech);
    }
}
