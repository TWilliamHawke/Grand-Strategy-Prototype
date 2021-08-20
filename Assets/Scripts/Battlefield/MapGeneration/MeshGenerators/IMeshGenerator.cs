using Battlefield.Generator;
using UnityEngine;

public interface IMeshGenerator
{
	void CreateVertices(ChunkGenerator chunkGenerator);
	void SetNext(IMeshGenerator nextGenerator);
}