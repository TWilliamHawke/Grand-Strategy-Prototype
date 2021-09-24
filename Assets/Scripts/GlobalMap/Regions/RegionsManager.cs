using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using UnityEngine;

namespace GlobalMap.Regions
{
    public class RegionsManager : MonoBehaviour
    {
		[SerializeField] RegionsList _regionsList;
		[SerializeField] Region _regionPrefab;
		[SerializeField] GeneratorConfig _config;

        public void AddPoint(Color color, int x, int z)
        {
            if (!_regionsList.regions.ContainsKey(color))
            {
                var region = Instantiate(_regionPrefab, _config.positionOffset, Quaternion.identity);
                region.transform.SetParent(transform);
                region.color = color;
                _regionsList.regions.Add(color, region);
            }
            _regionsList.regions[color].AddPoint(x, z);

        }


    }
}