using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Espionage
{
	[CreateAssetMenu(fileName ="GuardState", menuName ="Global map/Guard State")]
	public class GuardState : ScriptableObject
	{
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[SerializeField] string _title;
		[Multiline(4)]
		[SerializeField] string _description;
		[Header("Values")]
	    [SerializeField] float _actionCostPct;
		[SerializeField] int _actionsSuccessChance;
		[SerializeField] float _visibilityPerActionPct;
		[SerializeField] int _visibilityCap = 100;
		[SerializeField] int _visibilityPerMonth = -5;

		public string title => _title;
		public Sprite icon => _icon;
	    public float actionCostPct => _actionCostPct;
		public float visibilityPerActionPct => _visibilityPerActionPct;
		public int visibilityCap => _visibilityCap;
		public int actionsSuccessChance => _actionsSuccessChance;
		public int visibilityPerMonth => _visibilityPerMonth;

	}
}