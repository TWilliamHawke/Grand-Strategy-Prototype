using System.Collections;
using System.Collections.Generic;
using GlobalMap.Regions;
using UnityEngine;

namespace PathFinding
{
    public class RegionNode : Node
    {
        Region _region;
        public Region region => _region;

        HashSet<RegionNode> _landNeighbors = new HashSet<RegionNode>();
        HashSet<RegionNode> _seaNeighbors = new HashSet<RegionNode>();

        public HashSet<RegionNode> neighbors => _landNeighbors;
        public HashSet<RegionNode> seaNeighbors => _seaNeighbors;

        public RegionNode(Region region)
        {
            _region = region;
            _nodeCenter = region.centerPosition;
            _position = new Vector2(_nodeCenter.x, _nodeCenter.z);
        }

        public void AddNeighbor(Region region)
        {
            if(region.isSeaRegion)
            {
                _seaNeighbors.Add(region.node);
            }
            else
            {
                _landNeighbors.Add(region.node);
            }
        }

    }
}