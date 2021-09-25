using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Factions
{
	public class FactionManager : MonoBehaviour
	{

		[SerializeField] List<FactionData> _factions = new List<FactionData>();

		List<Faction> _factionsList = new List<Faction>();

		public List<Faction> factionsList => _factionsList;

	    void Awake()
	    {
	        foreach (var dataObject in _factions)
			{
				_factionsList.Add(new Faction(dataObject));
			}
	    }
	
	}
}