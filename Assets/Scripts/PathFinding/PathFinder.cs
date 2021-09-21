using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class PathFinder<T> where T : Node
    {
        INodeList<T> _battlefieldData;

        T _startNode;
        T _targetNode;

        List<T> _sortedNodes = new List<T>();
        List<T> _unsortedNodes = new List<T>();

        public PathFinder(T startNode, T targetNode, INodeList<T> battlefieldData)
        {
            _startNode = startNode;
            _targetNode = targetNode;

            _battlefieldData = battlefieldData;
        }

        public Stack<T> GetPath()
        {
            var path = new Stack<T>();
            _targetNode.parent = null;

            _unsortedNodes.Add(_startNode);

            CheckNodes();

            if (_targetNode.parent != null)
            {
                var pathPoint = _targetNode;
                while (pathPoint != _startNode)
                {
                    path.Push(pathPoint);
                    pathPoint = pathPoint.parent as T;

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
                var nearestNode = FindNearestNodeFromUnsorted();
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

        T FindNearestNodeFromUnsorted()
        {
            T nearestNode = _unsortedNodes[0];

            foreach (var node in _unsortedNodes)
            {
                if (IsnodeClose(nearestNode, node))
                {
                    nearestNode = node;
                }
            }
            return nearestNode;
        }

        bool IsnodeClose(T selectedNode, T candidate)
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