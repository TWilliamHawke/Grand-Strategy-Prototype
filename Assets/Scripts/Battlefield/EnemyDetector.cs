using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

namespace Battlefield
{
    [RequireComponent(typeof(Troop))]
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] BattlefieldData _battlefieldData;

        ChunkNode _currentNode;
        IEnumerable<ChunkNode> _neightborNodes = new List<ChunkNode>();

        //data for stateMachine
        public Directions threadDirection { get; private set; } = Directions.south;
        public bool IsAttacked { get; private set; } = false;
        Troop _thread;

        //cache
        Troop _troopData;
        UnitsController _unitsController;

        private void Awake()
        {
            _troopData = GetComponent<Troop>();
            _unitsController = GetComponent<UnitsController>();
        }

        void Start()
        {
            FillNeightborNodes();
        }

        void FillNeightborNodes()
        {
            _currentNode = _battlefieldData.FindNode(this.gameObject);
            _neightborNodes = _battlefieldData.FindNeightborNodes(_currentNode);

            _currentNode.OnTroopEnter += CheckTroopEnteredInNode;
            foreach(var node in _neightborNodes)
            {
                node.OnTroopEnter += CheckTroopEnteredInNode;
            }
        }


        void CheckTroopEnteredInNode(Troop troop, Node node)
        {
            if(node == _currentNode)
            {
                IsAttacked = true;
                troop.enemy = _unitsController;

            }
            else
            {
                if(_battlefieldData.FindDirection(_currentNode, node, out var direction))
                {
                    threadDirection = direction;
                }
            }
        }

        void CheckTroopTargetingToNode(Troop troop, Node node)
        {
            if(node == _currentNode)
            {
                if (_battlefieldData.FindDirection(node, _currentNode, out var direction))
                {
                    threadDirection = direction;
                }
            }
        }
    }
}