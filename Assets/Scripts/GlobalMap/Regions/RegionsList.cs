using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using PathFinding;
using UnityEngine;

namespace GlobalMap.Regions
{
    [CreateAssetMenu(fileName = "RegionsList", menuName ="Global map/Regions List")]
    public class RegionsList : ScriptableObject, INodeList<RegionNode>
    {
        Dictionary<Color, Region> _regions = new Dictionary<Color, Region>();
        public Dictionary<Color, Region> regions => _regions;

        public void CreateRegionsMeshes()
        {
            foreach(var pair in _regions)
            {
                pair.Value.FindCenterPosition();
                pair.Value.GenerateMesh();
                pair.Value.SetRegionColor();
            }
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

        public IEnumerable<RegionNode> FindNeightborNodes(RegionNode node)
        {
            return node.neighbors;
        }
    }
}