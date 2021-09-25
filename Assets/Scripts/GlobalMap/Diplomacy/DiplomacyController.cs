using System.Collections;
using System.Collections.Generic;
using GlobalMap.Factions;
using UnityEngine;

namespace GlobalMap.Diplomacy
{
	[CreateAssetMenu(fileName = "DiplomacyController", menuName = "Global Map/DiplomacyController")]
	public class DiplomacyController : ScriptableObject
	{
	    [SerializeField] FactionData _playerFaction;
		[SerializeField] FactionData _targetFaction;

		public FactionData playerFaction => _playerFaction;
		public FactionData targetFaction => _targetFaction;
	}
}