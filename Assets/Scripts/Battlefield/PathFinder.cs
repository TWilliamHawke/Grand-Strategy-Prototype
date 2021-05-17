using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    public class PathFinder
    {
        BattlefieldData _battlefieldData;

        Node _startNode;
        Node _targetNode;

        List<Node> _sortedNodes = new List<Node>();
        List<Node> _unsortedNodes = new List<Node>();

        public PathFinder(Square startSquare, Square targetSquare, BattlefieldData battlefieldData)
        {
            _startNode = battlefieldData.FindNode(startSquare);
            _targetNode = battlefieldData.FindNode(targetSquare);

            _battlefieldData = battlefieldData;
        }

        public Stack<Node> GetPath()
        {
            var path = new Stack<Node>();
            _targetNode.parent = null;

            _unsortedNodes.Add(_startNode);

            CheckNodes();

            if (_targetNode.parent != null)
            {
                var pathPoint = _targetNode;
                while (pathPoint != _startNode)
                {
                    path.Push(pathPoint);
                    pathPoint = pathPoint.parent;

                    if (path.Count > 80)
                    {
                        Debug.LogError("Something goes wrong!!! Path is too long!!");
                        break;
                    }
                }
            }

            return path;
        }

        void CheckNodes()
        {
            while (_unsortedNodes.Count > 0)
            {
                var nearestNode = FindNearestNode();
                var neightborNodes = _battlefieldData.FindNeightborNodes(nearestNode);

                foreach (var node in neightborNodes)
                {
                    if (_unsortedNodes.Contains(node) || _sortedNodes.Contains(node))
                    {
                        continue;
                    }
                    node.parent = nearestNode;
                    if (node == _targetNode)
                    {
                        return;
                    }

                    node.startDist = node.GetDistanceFrom(_startNode);
                    node.targetDist = node.GetDistanceFrom(_targetNode);
                    _unsortedNodes.Add(node);
                }

                _unsortedNodes.Remove(nearestNode);
                _sortedNodes.Add(nearestNode);
            }
        }

        Node FindNearestNode()
        {
            Node nearestNode = _unsortedNodes[0];

            foreach (var node in _unsortedNodes)
            {
                if (IsnodeClose(nearestNode, node))
                {
                    nearestNode = node;
                }
            }
            return nearestNode;
        }

        bool IsnodeClose(Node selectedNode, Node candidate)
        {
            if (selectedNode.totalDist < candidate.totalDist)
            {
                return false;
            }
            if (selectedNode.totalDist == candidate.totalDist && selectedNode.targetDist < candidate.targetDist)
            {
                return false;
            }
            return true;
        }
    }

}