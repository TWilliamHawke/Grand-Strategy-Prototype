using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Regions
{
    public class RegionsList : MonoBehaviour
    {
        [SerializeField] Region _regionPrefab;

        Dictionary<Color, Region> _regions = new Dictionary<Color, Region>();

        public void CreateRegionsMeshes()
        {
            foreach(var pair in _regions)
            {
                pair.Value.FindCenterPosition();
                pair.Value.GenerateMesh();
                pair.Value.SetRegionColor();
            }
        }

        public void AddPoint(Color color, int x, int z, Vector3 offset)
        {
            if (!_regions.ContainsKey(color))
            {
                var fixedOffset = offset + Vector3.up * 0.5f;
                var region = Instantiate(_regionPrefab, -fixedOffset, Quaternion.identity);
                region.transform.SetParent(transform);
                region.color = color;
                _regions.Add(color, region);
            }
            _regions[color].AddPoint(x, z);

        }

        public void AddNeighbors(Color c1, Color c2)
        {
            bool success1 = _regions.TryGetValue(c1, out var region1);
            bool success2 = _regions.TryGetValue(c2, out var region2);

            if (success1 && success2)
            {
                region1.AddNeighbor(region2);
                region2.AddNeighbor(region1);
            }
        }

    }
}