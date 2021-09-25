using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "factionName", menuName = "Global Map/Faction")]
public class FactionData : ScriptableObject
{
    [SerializeField] string _factionName = " EmptyName";
    [SerializeField] List<FactionData> _startEnemies;
    [SerializeField] Color _factionColor = Color.white;
    [SerializeField] bool _isPlayerFaction = false;
    [SerializeField] Sprite _coatOfArms;

    public string factionName => _factionName;
    public Color factionColor => _factionColor;
    public bool isPlayerFaction => _isPlayerFaction;
    public List<FactionData> startEnemies => _startEnemies;
    public Sprite coatOfArms => _coatOfArms;



    List<Technology> _researchedTechnology = new List<Technology>();
    List<Technology> researchedTechnology => _researchedTechnology;

    void OnEnable()
    {
        _researchedTechnology.Clear();
    }

    public void AddTechnology(Technology tech)
    {
        _researchedTechnology.Add(tech);
    }

}
