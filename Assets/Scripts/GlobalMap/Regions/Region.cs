using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
using GlobalMap.Generator;
using PathFinding;
using UnityEngine;

namespace GlobalMap.Regions
{
	public class Region : MonoBehaviour
	{
		[SerializeField] RegionMesh _meshGenerator;
		[SerializeField] Castle _castle;
		[SerializeField] ChunkArrow _arrow;
		[SerializeField] GeneratorConfig _config;
		[Range(0, 1)]
		[SerializeField] float _castleOffsetY;

		public Color color { get; set; }

		Vector3 _regionCenter;
		RegionNode _node;

		//getters
		public Vector3 centerPosition => _regionCenter;
		public bool isSeaRegion => _regionCenter.y <= 0;
		public bool isUnWalkable => color == Color.black || color == Color.white;
		public Castle castle => _castle;
		public RegionNode node => _node;


		public void AddPoint(int x, int z)
		{
			_meshGenerator.AddPoint(x, z);
		}

		public void FindCenterPosition()
		{
			_regionCenter = _meshGenerator.FindCenterPosition() + _config.positionOffset;
			_node = new RegionNode(this);
			if(isUnWalkable || isSeaRegion)
			{
				_castle.gameObject.SetActive(false);
			}
			else
			{
				var position = _regionCenter + Vector3.up * _castleOffsetY;
				_castle.transform.position = position;
				_arrow.transform.position = position;
			}
		}

		public void AddNeighbor(Region neighBor)
		{
			if(neighBor == this || neighBor.isUnWalkable) return;
			_node.AddNeighbor(neighBor);
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

		public void RotatePathArrow(RegionNode node)
		{
			_arrow.gameObject.SetActive(true);
			_arrow.transform.rotation = Quaternion.FromToRotation(Vector3.forward, node.nodeCenter - _node.nodeCenter);
			_arrow.UpdateShape(_config.terrainLayer);
		}

		public void HidePathArrow()
		{
			_arrow.gameObject.SetActive(false);
		}
	}
}