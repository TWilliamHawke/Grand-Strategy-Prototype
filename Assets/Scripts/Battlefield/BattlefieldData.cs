using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Battlefield.Chunks;
using Battlefield.Generator;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "BattleFieldData", menuName = "Battlefield/Battlefield Data")]

    public class BattlefieldData : ScriptableObject
    {
        Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
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
            var node = new Node(chunk, _mapConfig.chunkSize);
            _nodes.Add(node.position.ToString(), node);
        }

        public Node FindNode(Chunk chunk)
        {
            return FindNode(chunk.gameObject);
        }

        public Node FindNode(GameObject gameobject)
        {
            var x = gameobject.transform.position.x;
            var z = gameobject.transform.position.z;
            return FindNode(new Vector2(x, z).ToString());
        }

        public List<Node> FindNeightborNodes(Chunk chunk)
        {
            var node = FindNode(chunk);
            return FindNeightborNodes(node);
        }

        public List<Node> FindNeightborNodes(Node node)
        {
            var neightborSquares = new List<Node>();
            var gridPos = node.position;

            foreach (var pair in _neightbors)
            {
                var newPos = pair.Value + gridPos;

                if (_nodes.TryGetValue(newPos.ToString(), out var neightbor))
                {
                    neightborSquares.Add(neightbor);
                }
            }
            return neightborSquares;
        }

        public bool FindDirection(Node from, Node to, out Directions direction)
        {
            if (from == null || to == null)
            {
                direction = Directions.north;
                return false;
            }

            var shift = to.position - from.position;

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

        Node FindNode(string position)
        {
            if (_nodes.TryGetValue(position, out var node))
            {
                return node;
            }
            else
            {
                Debug.LogError("Square is not found on battlefield");
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