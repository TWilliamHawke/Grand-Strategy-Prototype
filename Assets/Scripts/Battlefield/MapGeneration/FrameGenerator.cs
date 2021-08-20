using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
	public class FrameGenerator : MonoBehaviour
	{
		[SerializeField] MapConfig _mapConfig;

        Mesh _mesh;
        Vector3[] _vertices;
        int[] _triangles;



		private void OnValidate() {
			GenerateLine(Vector3.zero, new Vector3(0, 0, 10));
		}

		public void GenerateLine(Vector3 start, Vector3 end)
		{
			Debug.Log("generate");

			for (int i = 0; i < _mapConfig.chunkLines; i++)
			{
				var point = Vector3.Lerp(start, end, i / (float)_mapConfig.chunkSize);
				print(point);
			}
		}
	}
}