using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlefield.Generator
{
    public class FlatMeshGenerator : IMeshGenerator
    {
        MapConfig _mapConfig;
        IMeshGenerator _nextGenerator;

        public FlatMeshGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void SetNext(IMeshGenerator nextGenerator)
        {
            _nextGenerator = nextGenerator;
        }

        public void CreateVertices(ChunkGenerator chunkGenerator)
        {
            if (chunkGenerator.corners.Length < 4)
            {
                var vertices = CreateVertices(1);
                chunkGenerator.SetVertices(vertices);
            }
            else if (CornersHaveSameHeight(chunkGenerator.corners))
            {
                var height = chunkGenerator.corners[0].y;
                var vertices = CreateVertices(height);
                chunkGenerator.SetVertices(vertices);
            }
            else
            {
                _nextGenerator.CreateVertices(chunkGenerator);
            }
        }


        Vector3[] CreateVertices(float heightLevel)
        {
            var vertices = new Vector3[_mapConfig.chunkLines * _mapConfig.chunkLines];
            var y = heightLevel * _mapConfig.heightPerLevel;

            for (int i = 0, x = 0; x <= _mapConfig.chunkSize; x++)
            {
                for (int z = 0; z <= _mapConfig.chunkSize; z++)
                {
                    vertices[i] = new Vector3(x, y, z) + ChunkGenerator.meshOffset;
                    i++;
                }
            }

            return vertices;

        }

        bool CornersHaveSameHeight(Vector3[] corners)
        {
            return corners.All((corner) => corner.y == corners[0].y);
        }

    }
}
