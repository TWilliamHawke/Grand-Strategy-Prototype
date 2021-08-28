using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] Troop _enemyTroopPrefab;
        [SerializeField] Troop _playerTroopPrefab;

        [SerializeField] MapConfig _mapConfig;
        [SerializeField] BattlefieldData _battlefieldGrid;

        private void Awake()
        {
            Generator.BattlefieldGenerator.OnGenerationFinish += SpawnUnits;
        }

        public void SpawnUnits()
        {
            if (_mapConfig.gridSize < 1) return;

            if (_mapConfig.gridSize < 3)
            {
                SpawnUnit(_playerTroopPrefab, 0, 0);
            }
            else if (_mapConfig.gridSize < 6)
            {
				SpawnUnit(_playerTroopPrefab, 2, 0);
				SpawnUnit(_enemyTroopPrefab, 2, 2);
            }
            else
            {
				SpawnUnit(_playerTroopPrefab, 2, 1);
				SpawnUnit(_enemyTroopPrefab, 2, 3);
				SpawnUnit(_enemyTroopPrefab, 3, 4);
				SpawnUnit(_enemyTroopPrefab, 4, 3);

            }
        }

        void SpawnUnit(Troop prefab, int nodeX, int nodeZ)
        {
            float x = nodeX * _mapConfig.chunkSize;
            float z = nodeZ * _mapConfig.chunkSize;

            var node = _battlefieldGrid.FindNode(new Vector3(x, 0, z));
            var spawnPosition = Raycasts.VerticalDown(new Vector3(node.position.x, 0, node.position.y), _mapConfig.gridLayer);

            var troop = Instantiate(prefab, spawnPosition, Quaternion.identity);
            
			troop.FindCurrentNode();
            troop.NormalizeUnitPositions();

            if (prefab == _enemyTroopPrefab)
            {
                node.enemyInNode = true;
                troop.SetRotation(Directions.south);
            }

        }
    }
}