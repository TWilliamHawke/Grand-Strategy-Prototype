using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class BattlefieldGenerator : MonoBehaviour
    {
        byte[,] _heightMap;

        [SerializeField] MapConfig _mapConfig;

        public BattlefieldGenerator(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public void StartGeneration()
        {
            GenerateHeightMap();


        }

        void GenerateHeightMap()
        {
            _heightMap = _mapConfig.generationAlgorithm.GenerateHeightMap();
        }

        void GenerateChunks()
        {
            for (int x = 0; x < _mapConfig.mapSize; x++)
            {
                for (int y = 0; y < _mapConfig.mapSize; y++)
                {
                    var chunkPos = new Vector3(x, 0, y) * _mapConfig.chunkSize;
                    var corners = new byte[4] {
                        _heightMap[x, y],
                        _heightMap[x + 1, y],
                        _heightMap[x + 1, y + 1],
                        _heightMap[x, y + 1] };

                    var chunk = new ChunkGenerator(corners, chunkPos, _mapConfig.chunkSize);
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