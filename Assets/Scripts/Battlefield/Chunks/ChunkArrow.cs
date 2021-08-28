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
	}
}