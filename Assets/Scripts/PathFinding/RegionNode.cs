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

        public RegionNode(Region region)
        {
            _region = region;
            _nodeCenter = region.centerPosition;
            _position = new Vector2(_nodeCenter.x, _nodeCenter.z);
        }



    }
}