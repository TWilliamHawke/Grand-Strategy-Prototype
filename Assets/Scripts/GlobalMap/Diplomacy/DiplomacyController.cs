using System.Collections;
using System.Collections.Generic;
using GlobalMap.Factions;
using UnityEngine;
using UnityEngine.Events;

namespace GlobalMap.Diplomacy
{
    [CreateAssetMenu(fileName = "DiplomacyController", menuName = "Global Map/DiplomacyController")]
    public class DiplomacyController : ScriptableObject
    {
        Faction _playerFaction;
        Faction _targetFaction;

        public Faction playerFaction { get; set; }
        public Faction targetFaction => _targetFaction;

        public event UnityAction<Faction> OnFactionSelect;

        public void SetTargetFaction(Faction faction)
        {
            _targetFaction = faction;
            OnFactionSelect?.Invoke(faction);
        }
    }
}