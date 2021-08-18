using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
	public class ChunkGenerator
	{
		byte[] cornersHeight;
		Vector3 zeroPosition;
		int chunkSize;

        public ChunkGenerator(byte[] cornersHeight, Vector3 zeroPosition, int chunkSize)
        {
            this.cornersHeight = cornersHeight;
            this.zeroPosition = zeroPosition;
            this.chunkSize = chunkSize;
        }

        public void CreateMesh()
        {

        }
    }
}