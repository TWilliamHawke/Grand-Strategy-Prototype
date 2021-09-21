using System.Collections;
using System.Collections.Generic;
using GlobalMap.Generator;
using PathFinding;
using UnityEngine;

namespace GlobalMap.Regions
{
	public class Region : MonoBehaviour
	{
		[SerializeField] RegionMesh _meshGenerator;
		[SerializeField] Castle _castle;
		[SerializeField] GeneratorConfig _config;
		[Range(0, 1)]
		[SerializeField] float _castleOffsetY;

		public Color color { get; set; }

		Vector3 _regionCenter;
		RegionNode _node;

		List<RegionNode> _landNeightbors = new List<RegionNode>();
		List<RegionNode> _seaNeightbors = new List<RegionNode>();

		//getters
		public Vector3 centerPosition => _regionCenter;
		public bool isSeaRegion => _regionCenter.y <= 0;
		public bool isUnWalkable => color == Color.black || color == Color.white;
		public List<RegionNode> landNeightbors => _landNeightbors;
		public Castle castle => _castle;


		public void AddPoint(int x, int z)
		{
			_meshGenerator.AddPoint(x, z);
		}

		public void FindCenterPosition()
		{
			_regionCenter = _meshGenerator.FindCenterPosition();
			_node = new RegionNode(this);
			if(isUnWalkable || isSeaRegion)
			{
				_castle.gameObject.SetActive(false);
			}
			else
			{
				var position = _regionCenter + _config.positionOffset + Vector3.up * _castleOffsetY;
				_castle.transform.position = position;
			}
		}

		public void AddNeighbor(Region neightBor)
		{
			
		}

		public void GenerateMesh()
		{
			if (isSeaRegion) return;

			_meshGenerator.GenerateMesh();
		}

		public void SetRegionColor()
		{
			_meshGenerator.SetColor(color);
		}
	}
}