using System.Collections;
using System.Collections.Generic;
using GlobalMap.Espionage;
using GlobalMap.Factions;
using UnityEngine;
using UnityEngine.Events;

namespace GlobalMap.Diplomacy
{
    [CreateAssetMenu(fileName = "DiplomacyController", menuName = "Global Map/DiplomacyController")]
    public class DiplomacyController : ScriptableObject
    {
        [SerializeField] SpyNetworkController _spyNetworkController;
        
        Faction _playerFaction;
        Faction _targetFaction;

        public Faction playerFaction { get; set; }
        public Faction targetFaction => _targetFaction;

        public event UnityAction<Faction> OnFactionSelect;

        public void SetTargetFaction(Faction faction)
        {
            _targetFaction = faction;
            _spyNetworkController.SetNetworkData(faction);
            OnFactionSelect?.Invoke(faction);
        }
    }
}