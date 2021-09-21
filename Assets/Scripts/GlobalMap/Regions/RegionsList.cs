using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using PathFinding;
using UnityEngine;

namespace GlobalMap.Regions
{
    public class RegionsList : MonoBehaviour, INodeList<RegionNode>
    {
        [SerializeField] Region _regionPrefab;
        [SerializeField] GeneratorConfig _config;

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

        public void AddPoint(Color color, int x, int z)
        {
            if (!_regions.ContainsKey(color))
            {
                var region = Instantiate(_regionPrefab, _config.positionOffset, Quaternion.identity);
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

        public List<RegionNode> FindNeightborNodes(RegionNode node)
        {
            return node.region.landNeightbors;
        }
    }
}