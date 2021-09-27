using System.Collections;
using System.Collections.Generic;
using GlobalMap.Diplomacy;
using GlobalMap.Espionage;
using UnityEngine;

namespace GlobalMap.Factions
{
    public class FactionRelations
    {
        Faction _faction;
        Dictionary<DiplomacyState, HashSet<FactionData>> _diplomacyState = new Dictionary<DiplomacyState, HashSet<FactionData>>();
        Dictionary<Faction, SpyNetworkData> _spyNetworks = new Dictionary<Faction, SpyNetworkData>();


        public FactionRelations(Faction faction, FactionData factionData)
        {
            _faction = faction;
            AddStartEnemies(factionData);
        }

        public void AddDiplomacyStateWithFaction(DiplomacyState state, FactionData faction)
        {

            if (_diplomacyState.TryGetValue(state, out var list))
            {
                if (list.Contains(faction)) return;
            }
            else
            {
                _diplomacyState.Add(state, new HashSet<FactionData>());
            }
            _diplomacyState[state].Add(faction);
        }

        void AddStartEnemies(FactionData factionData)
        {
            foreach (var faction in factionData.startEnemies)
            {
                AddDiplomacyStateWithFaction(DiplomacyState.war, faction);
            }
        }

        public void CreateSpyNetwork(Faction targetFaction)
        {
            if(targetFaction == _faction) return;
            if(_spyNetworks.ContainsKey(targetFaction)) return;

            _spyNetworks.Add(targetFaction, new SpyNetworkData(targetFaction));
        }

        public void DestroySpyNetWork(Faction targetFaction)
        {
            _spyNetworks.Remove(targetFaction);
        }

    }
}