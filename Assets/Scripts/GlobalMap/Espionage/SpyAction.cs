using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Espionage
{
	[CreateAssetMenu(fileName ="SpyAction", menuName ="Global map/SpyAction")]
	public class SpyAction : ScriptableObject
	{
	    [SerializeField] string _title;
		[Multiline(4)]
		[SerializeField] string _description;

		[SerializeField] float _baseCost;
		[SerializeField] float _baseChance;
		[SerializeField] float _baseVisibility;
	}
}