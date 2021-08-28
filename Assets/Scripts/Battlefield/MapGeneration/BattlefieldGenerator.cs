using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield.Generator
{
    [RequireComponent(typeof(UnitSpawner))]
    public class BattlefieldGenerator : MonoBehaviour
    {
        byte[,] _heightMap;

        public static event UnityAction OnGenerationFinish;

        [SerializeField] MapConfig _mapConfig;
        [SerializeField] BattlefieldData _battlefieldData;
        [SerializeField] ChunkGenerator _chunkPrefab;
        [SerializeField] MeshRenderer[] _trees;

        UnitSpawner _unitSpawner;
        System.Random _generator;


        private void Awake()
        {
            _unitSpawner = GetComponent<UnitSpawner>();
        }

        void Start()
        {
            _generator = new System.Random(_mapConfig.seed);
            GenerateHeightMap();
            GenerateChunks();

            OnGenerationFinish?.Invoke();

        }

        void OnDrawGizmos()
        {
            // GenerateHeightMap();
            // DrawHeigtMapGizmos();
        }



        void GenerateHeightMap()
        {
            _heightMap = _mapConfig.generationAlgorithm.GenerateHeightMap(_generator);
        }

        void GenerateChunks()
        {
            var flatGenerator = new FlatMeshGenerator(_mapConfig);
            var innerGenerator = new InnerSlopeGenerator(_mapConfig);
            var outerGenerator = new OuterSlopeGenerator(_mapConfig);
            var hollowGenerator = new HollowGenerator(_mapConfig);
            var slopeGenerator = new SlopeGenerator(_mapConfig);

            var forestGenerator = new ForestGenerator(_trees, _mapConfig);

            flatGenerator.SetNext(innerGenerator);
            innerGenerator.SetNext(outerGenerator);
            outerGenerator.SetNext(hollowGenerator);
            hollowGenerator.SetNext(slopeGenerator);

            float bg = _mapConfig.chunksBehindGrid;

            for (int x = 0; x < _mapConfig.linesCount - 1; x++)
            {
                for (int y = 0; y < _mapConfig.linesCount - 1; y++)
                {
                    var chunkPos = new Vector3(x - bg, 0, y - bg) * _mapConfig.chunkSize;
                    Vector3[] corners = GetChunkCorners(x, y);

                    var chunk = Instantiate(_chunkPrefab, chunkPos, Quaternion.identity);
                    chunk.SetCorners(corners);
                    flatGenerator.CreateVertices(chunk);
                    chunk.transform.SetParent(transform);
                    forestGenerator.CreateForest(chunk);

                    if (ChunkInsideGrid(x, y))
                    {
                        chunk.CreateFrame();
                    }

                }
            }

        }

        private Vector3[] GetChunkCorners(int x, int y)
        {
            float sz = _mapConfig.chunkSize;

            return new Vector3[4] {
                        new Vector3(0, _heightMap[x, y], 0),
                        new Vector3(sz, _heightMap[x + 1, y], 0),
                        new Vector3(sz, _heightMap[x + 1, y + 1], sz),
                        new Vector3(0, _heightMap[x, y + 1], sz)
                        };
        }

        private void DrawHeigtMapGizmos()
        {
            for (int x = 0; x < _mapConfig.linesCount; x++)
            {
                for (int z = 0; z < _mapConfig.linesCount; z++)
                {
                    int height = _heightMap[x, z];

                    if (height > 1)
                    {
                        Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one * 0.8f);
                    }
                    else
                    {
                        Gizmos.DrawWireCube(new Vector3(x, 0, z), Vector3.one * 0.5f);
                    }
                }
            }
        }

        bool ChunkInsideGrid(int x, int z)
        {
            if (x < _mapConfig.chunksBehindGrid || z < _mapConfig.chunksBehindGrid) return false;
            int xyMax = _mapConfig.chunksBehindGrid + _mapConfig.gridSize;

            if (x >= xyMax || z >= xyMax) return false;

            return true;
        }
    }
}