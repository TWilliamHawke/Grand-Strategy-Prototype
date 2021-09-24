using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battlefield.Chunks;
using Battlefield.Generator;
using PathFinding;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "BattleFieldData", menuName = "Battlefield/Battlefield Data")]

    public class BattlefieldData : ScriptableObject, INodeList<ChunkNode>
    {
        Dictionary<string, ChunkNode> _nodes = new Dictionary<string, ChunkNode>();
        [SerializeField] MapConfig _mapConfig;
        int size => _mapConfig.chunkSize;

        Dictionary<Directions, Vector2> _neightbors;

        void OnEnable()
        {
            FillNeighborsDictionary();
        }

        private void OnDisable()
        {
            _nodes.Clear();
        }

        public void AddNode(Chunk chunk)
        {
            var node = new ChunkNode(chunk, _mapConfig.gridLayer);
            _nodes.Add(node.position2d.ToString(), node);
        }

        public ChunkNode FindNode(Chunk chunk)
        {
            return FindNode(chunk.gameObject.transform.position);
        }

        public ChunkNode FindNode(GameObject gameobject)
        {
            var x = gameobject.transform.position.x;
            var z = gameobject.transform.position.z;
            return FindNode(new Vector2(x, z).ToString());
        }

        public ChunkNode FindNode(Vector3 position)
        {
            var x = position.x;
            var z = position.z;
            // Debug.Log(new Vector2(x, z).ToString());
            return FindNode(new Vector2(x, z).ToString());
        }

        public IEnumerable<ChunkNode> FindNeightborNodes(Chunk chunk)
        {
            var node = FindNode(chunk);
            return FindNeightborNodes(node);
        }

        public IEnumerable<ChunkNode> FindNeightborNodes(ChunkNode node)
        {
            var neightborNodes = new List<ChunkNode>();
            var gridPos = node.position2d;

            foreach (var pair in _neightbors)
            {
                var newPos = pair.Value + gridPos;

                if (_nodes.TryGetValue(newPos.ToString(), out var neightbor))
                {
                    neightborNodes.Add(neightbor);
                }
            }
            return neightborNodes;
        }

        public bool FindDirection(Node from, Node to, out Directions direction)
        {
            if (from == null || to == null)
            {
                direction = Directions.north;
                return false;
            }

            var shift = to.position2d - from.position2d;

            var pair = _neightbors.FirstOrDefault(pair => pair.Value == shift);

            direction = pair.Key;
            return true;
        }


        public void HideAllInfo()
        {
            foreach (var node in _nodes)
            {
                node.Value.chunk.HidePathArrow();
            }
        }

        public Chunk FindChunk(GameObject gameobject)
        {
            return FindNode(gameobject)?.chunk;
        }

        ChunkNode FindNode(string position)
        {
            if (_nodes.TryGetValue(position, out var node))
            {
                return node;
            }
            else
            {
                Debug.LogError("Node is not found on battlefield");
                return null;
            }
        }

        void FillNeighborsDictionary()
        {
            _neightbors = new Dictionary<Directions, Vector2>()
            {
            { Directions.north,     Vector2.up * size },
            { Directions.northEast, (Vector2.up + Vector2.right) * size },
            { Directions.east,      Vector2.right * size },
            { Directions.southEast, (Vector2.right + Vector2.down) * size },
            { Directions.south,     Vector2.down * size },
            { Directions.southWest, (Vector2.down + Vector2.left) * size },
            { Directions.west,      Vector2.left * size },
            { Directions.northWest, (Vector2.up + Vector2.left) * size },
            };
        }

    }
}