using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Regions
{
	public class Region : MonoBehaviour
	{
		[SerializeField] RegionMesh _meshGenerator;

		public Color color { get; set; }

		Vector3 _regionCenter = Vector3.zero;

		bool isSeaRegion => _regionCenter.y <= 0;
		bool isUnWalkable => color == Color.black || color == Color.white;


		public void AddPoint(int x, int z)
		{
			_meshGenerator.AddPoint(x, z);
		}

		public void FindCenterPosition()
		{
			_regionCenter = _meshGenerator.FindCenterPosition();

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