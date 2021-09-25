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
    }
}