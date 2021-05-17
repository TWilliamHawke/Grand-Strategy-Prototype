using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "BattleFieldData", menuName = "Battlefield/Battlefield Data")]

    public class BattlefieldData : ScriptableObject
    {
        Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

        Dictionary<Directions, Vector2> neightbors = new Dictionary<Directions, Vector2>()
        {
            { Directions.north, Vector2.up },
            { Directions.northEast, Vector2.up + Vector2.right },
            { Directions.east, Vector2.right },
            { Directions.southEast, Vector2.right + Vector2.down },
            { Directions.south, Vector2.down },
            { Directions.southWest, Vector2.down + Vector2.left },
            { Directions.west, Vector2.left },
            { Directions.northWest, Vector2.up + Vector2.left },
        };

        private void OnDisable()
        {
            _nodes.Clear();
        }

        public void AddNode(Square square)
        {
            var node = new Node(square);
            _nodes.Add(square.StringifyPosition(), node);
        }

        public Node FindNode(Square square)
        {
            return FindNode(square.StringifyPosition());
        }

        public Node FindNode(Vector2 position)
        {
            return FindNode(position.ToString());
        }

        public Node FindNode(GameObject gameobject)
        {
            var x = Mathf.RoundToInt(gameobject.transform.position.x / 10);
            var z = Mathf.RoundToInt(gameobject.transform.position.z / 10);
            return FindNode(new Vector2(x, z).ToString());
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

        public List<Node> FindNeightborNodes(Square square)
        {
            var node = FindNode(square);
            return FindNeightborNodes(node);
        }

        public List<Node> FindNeightborNodes(Node node)
        {
            var neightborSquares = new List<Node>();
            var gridPos = node.square.GetPosition();

            foreach (var pair in neightbors)
            {
                var newPos = pair.Value + gridPos;

                if (_nodes.TryGetValue(newPos.ToString(), out var neightbor))
                {
                    neightborSquares.Add(neightbor);
                }
            }
            return neightborSquares;
        }

        public bool FindDirection(Square from, Square to, out Directions direction)
        {
            if (from == null || to == null)
            {
                direction = Directions.north;
                return false;
            }

            var shift = to.GetPosition() - from.GetPosition();

            var pair = neightbors.FirstOrDefault(pair => pair.Value == shift);

            direction = pair.Key;
            return true;
        }


        public void HideAllInfo()
        {
            foreach (var node in _nodes)
            {
                node.Value.square.HidePathArrow();
            }
        }

        public Square FindSquare(GameObject gameobject)
        {
            return FindNode(gameobject)?.square;
        }

        Square FindSquare(Vector2 position)
        {
            if (_nodes.TryGetValue(position.ToString(), out var node))
            {
                return node.square;
            }
            return null;
        }
    }
}