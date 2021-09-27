using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Factions
{
	public class Faction
	{
	    FactionData _factionData;
		FactionRelations _factionRelation;

		public FactionData factionData => _factionData;

        public Faction(FactionData factionData)
        {
            _factionData = factionData;
			_factionRelation = new FactionRelations(this, factionData);
        }

		public void CreateSpyNetwork(Faction faction) => _factionRelation.CreateSpyNetwork(faction);
		
		public void DestroySpyNetWork(Faction faction) => _factionRelation.DestroySpyNetWork(faction);
    }
}