using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
	//replace a unit spawner
	public class DemoMapSetup : MonoBehaviour
	{
		[SerializeField] Troop _playerTroop;
		[SerializeField] Troop[] _enemyTroops;

	    void Start()
	    {
			_playerTroop.FindCurrentNode();
	        foreach(var enemy in _enemyTroops)
			{
				enemy.SetRotation(Directions.south);
				var node = enemy.FindCurrentNode();
				node.enemyInNode = true;
			}
	    }
		}
}