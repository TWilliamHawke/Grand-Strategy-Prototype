using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalMap.Espionage;

[CreateAssetMenu(fileName = "factionName", menuName = "Global Map/Faction")]
public class FactionData : ScriptableObject
{
    [SpritePreview]
    [SerializeField] Sprite _coatOfArms;
    [SerializeField] string _factionName = " EmptyName";
    [SerializeField] Color _factionColor = Color.white;
    [SerializeField] bool _isPlayerFaction = false;
    [SerializeField] List<FactionData> _startEnemies;
    [SerializeField] GuardState[] _guardStates = new GuardState[4];

    public string factionName => _factionName;
    public Color factionColor => _factionColor;
    public bool isPlayerFaction => _isPlayerFaction;
    public List<FactionData> startEnemies => _startEnemies;
    public Sprite coatOfArms => _coatOfArms;
    public GuardState[] guardStates => _guardStates;


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
