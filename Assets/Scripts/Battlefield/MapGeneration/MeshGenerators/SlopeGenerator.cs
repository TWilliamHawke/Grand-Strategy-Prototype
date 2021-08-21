using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class SlopeGenerator : IMeshGenerator
    {
        MapConfig _mapConfig;

        public SlopeGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void SetNext(IMeshGenerator nextGenerator)
        {
            throw new System.Exception("this generator should be last int queue");
        }

        public void CreateVertices(ChunkGenerator chunkGenerator)
        {
            Vector3 lowestCorner1 = Vector3.up * 100;
            Vector3 lowestCorner2 = Vector3.up * 100;

            foreach (var corner in chunkGenerator.corners)
            {
                if (corner.y < lowestCorner1.y)
                {
                    lowestCorner1 = corner;
                }
                else if (corner.y == lowestCorner1.y)
                {
                    lowestCorner2 = corner;
                }
            }

            var vertices = new Vector3[_mapConfig.chunkLines * _mapConfig.chunkLines];


            for (int x = 0, i = 0; x <= _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z <= _mapConfig.chunkSize; z++)
                {
                    //operations with vectors
                    float x1 = lowestCorner2.x - x;
                    float z1 = lowestCorner2.z - z;
                    float x2 = lowestCorner1.x - lowestCorner2.x;
                    float z2 = lowestCorner1.z - lowestCorner2.z;

                    float distance = (Mathf.Abs(x1 * z2) + Mathf.Abs(z1 * x2)) / Mathf.Pow(_mapConfig.chunkSize, 2);

                    float height = (1 + _mapConfig.slopeCurve.Evaluate(distance)) * _mapConfig.heightPerLevel;
                    vertices[i] = new Vector3(x, height, z) + ChunkGenerator.meshOffset;
                    i++;

                }
            }

            chunkGenerator.SetVertices(vertices);

        }


    }
}