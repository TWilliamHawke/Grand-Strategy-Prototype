using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Espionage
{
    [CreateAssetMenu(fileName = "SpyNetworkLevels", menuName = "Global map/Spy Network Controller")]
    public class SpyNetworkController : ScriptableObject
    {

		[SerializeField] Color _networkLevelsColor = Color.green;
		[SerializeField] Color _visibilityLevelColor = Color.red;
		[SerializeField] float _spyPointsPerMonth = 5f;
		[SerializeField] float _visibilityDecreaces = 5f;

        [SerializeField] List<SpyNetworkLevel> _levels = new List<SpyNetworkLevel>();

		SpyNetworkData _targetFactionNetworkData;

        List<SpyNetworkLevel> levels => _levels;
		public Color networkLevelsColor => _networkLevelsColor;
		public float spyPointsPerMonth => _spyPointsPerMonth;
		public float visibilityDecreaces => _visibilityDecreaces;



    }
}