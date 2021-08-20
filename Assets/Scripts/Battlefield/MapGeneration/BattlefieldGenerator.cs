using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class BattlefieldGenerator : MonoBehaviour
    {
        byte[,] _heightMap;

        [SerializeField] MapConfig _mapConfig;
        [SerializeField] ChunkGenerator _chunkPrefab;

        public BattlefieldGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void StartGeneration()
        {
            GenerateHeightMap();

        }

        void Start()
        {
            GenerateHeightMap();
            GenerateChunks();

        }

        void GenerateHeightMap()
        {
            _heightMap = _mapConfig.generationAlgorithm.GenerateHeightMap();
        }

        void GenerateChunks()
        {
            var flatGenerator = new FlatMeshGenerator(_mapConfig);
            var innerGenerator = new InnerSlopeGenerator(_mapConfig);
            var outerGenerator = new OuterSlopeGenerator(_mapConfig);
            var hollowGenerator = new HollowGenerator(_mapConfig);
            var slopeGenerator = new SlopeGenerator(_mapConfig);

            flatGenerator.SetNext(innerGenerator);
            innerGenerator.SetNext(outerGenerator);
            outerGenerator.SetNext(hollowGenerator);
            hollowGenerator.SetNext(slopeGenerator);


            for (int x = 0; x < _mapConfig.mapSize; x++)
            {
                for (int y = 0; y < _mapConfig.mapSize; y++)
                {
                    float sz = _mapConfig.chunkSize;
                    var chunkPos = new Vector3(x, 0, y) * _mapConfig.chunkSize;

                    var corners = new Vector3[4] {
                        new Vector3(0, _heightMap[x, y], 0),
                        new Vector3(sz, _heightMap[x + 1, y], 0),
                        new Vector3(sz, _heightMap[x + 1, y + 1], sz),
                        new Vector3(0, _heightMap[x, y + 1], sz)
                        };

                    var chunk = Instantiate(_chunkPrefab, chunkPos, Quaternion.identity);
                    chunk.SetCorners(corners);
                    flatGenerator.CreateVertices(chunk);
                    chunk.CreateMesh();
                    chunk.transform.SetParent(transform);
                }
            }

        }

        private void OnDrawGizmos()
        {
            StartGeneration();
            DrawHeigtMapGizmos();

        }

        private void DrawHeigtMapGizmos()
        {
            for (int x = 0; x < _mapConfig.linesCount; x++)
            {
                for (int y = 0; y < _mapConfig.linesCount; y++)
                {
                    int height = _heightMap[x, y];

                    if (height > 1)
                    {
                        Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one * 0.8f);
                    }
                    else
                    {
                        Gizmos.DrawWireCube(new Vector3(x, 0, y), Vector3.one * 0.5f);
                    }
                }
            }
        }
    }
}