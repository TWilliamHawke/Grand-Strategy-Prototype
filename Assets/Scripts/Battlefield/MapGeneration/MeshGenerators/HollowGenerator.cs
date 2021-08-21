using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class HollowGenerator : IMeshGenerator
    {
        MapConfig _mapConfig;
        Vector3[] _corners;
        IMeshGenerator _nextGenerator;

        public void SetNext(IMeshGenerator nextGenerator)
        {
            _nextGenerator = nextGenerator;
        }


        public HollowGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void CreateVertices(ChunkGenerator chunkGenerator)
        {
            if (chunkGenerator.corners[0].y == chunkGenerator.corners[2].y)
            {
                var vertices = CreateVertices(chunkGenerator.corners);
                chunkGenerator.SetVertices(vertices);
            }
            else
            {
                _nextGenerator.CreateVertices(chunkGenerator);
            }
        }

        Vector3[] CreateVertices(Vector3[] corners)
        {
            var vertices = new Vector3[_mapConfig.chunkLines * _mapConfig.chunkLines];

            for (int i = 0, x = 0; x <= _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z <= _mapConfig.chunkSize; z++)
                {
                    var pivotPoint = x + z >= _mapConfig.chunkSize ? corners[0] : corners[2];

                    float distanceX = Mathf.Abs(pivotPoint.x - x);
                    float distanceZ = Mathf.Abs(pivotPoint.z - z);

                    float distance = Mathf.Min(distanceX, distanceZ) / _mapConfig.chunkSize;
                    float height = _mapConfig.slopeCurve.Evaluate(distance);

                    // calculate from hightestPoit
                    float y = (2 - height) * _mapConfig.heightPerLevel;

                    if (corners[0].y > corners[1].y)
                    {
                        //calculateFrom lowest point
                        y = (1 + height) * _mapConfig.heightPerLevel;
                    }

                    vertices[i] = new Vector3(x, y, z) + ChunkGenerator.meshOffset;
                    i++;
                }
            }

            return vertices;

        }

        Vector3[] CreateVertices(float heightLevel)
        {
            var vertices = new Vector3[_mapConfig.chunkLines * _mapConfig.chunkLines];
            float y = heightLevel * _mapConfig.heightPerLevel;

            for (int i = 0, x = 0; x <= _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z <= _mapConfig.chunkSize; z++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }

            return vertices;

        }

    }
}