using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class ForestGenerator
    {
        MeshRenderer[] _trees;
        MapConfig _mapConfig;
        System.Random _generator;

		float _baseTreeScale = 0.3f;

        public ForestGenerator(MeshRenderer[] trees, MapConfig mapConfig)
        {
            _trees = trees;
            _mapConfig = mapConfig;
            _generator = new System.Random(_mapConfig.seed);
        }

        public void CreateForest(ChunkGenerator chunk)
        {
			if(_generator.Next(0, 100) > _mapConfig.forestChance) return;

            int offset = (_mapConfig.chunkSize - 1) / 2;
			var offsetVector = new Vector3(-offset, 0, -offset);

            for (int x = 0; x < _mapConfig.chunkSize; x+=2)
            {
                for (int z = 0; z < _mapConfig.chunkSize; z+=2)
                {
                    if (_generator.Next(0, 100) > _mapConfig.forestDensity) continue;

					var treePrefabIndex = _generator.Next(0, _trees.Length);

                    var spawnPos = chunk.transform.position + new Vector3(x, 0, z) + offsetVector;
                    spawnPos = Raycasts.VerticalDown(spawnPos, _mapConfig.gridLayer);

					var randomScaleFactor = 0.75f + 0.25f * (float)_generator.NextDouble();
                    var scale = Vector3.one * _baseTreeScale * randomScaleFactor;
					var angleY = _generator.Next(0, 359);
					var rotation = Quaternion.Euler(0, angleY, 0);

					var tree = GameObject.Instantiate(_trees[treePrefabIndex], spawnPos, rotation);
					tree.transform.SetParent(chunk.transform);
					tree.transform.localScale = scale;

                }
            }
        }
    }
}