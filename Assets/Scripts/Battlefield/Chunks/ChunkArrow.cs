using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield.Generator;

namespace Battlefield.Chunks
{
	public class ChunkArrow : MonoBehaviour
	{
		[SerializeField] ArrowMeshTransformation _part1;
		[SerializeField] ArrowMeshTransformation _part2;
	
	    public void UpdateShape()
		{
			_part1?.ChangeShape(transform.rotation.eulerAngles.y);
			_part2?.ChangeShape(transform.rotation.eulerAngles.y);
		}

		public void UpdateShape(LayerMask layerMask)
		{
			float sin = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.PI / 180);
			float cos = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.PI / 180);

			_part1?.ChangeShape(cos, sin, layerMask);
			_part2?.ChangeShape(sin, cos, layerMask);
		}
	}
}