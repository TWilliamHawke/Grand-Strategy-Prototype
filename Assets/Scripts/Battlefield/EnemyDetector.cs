using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    [RequireComponent(typeof(Troop))]
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] BattlefieldData _battlefieldData;

        Node _currentNode;
        List<Node> _neightborNodes = new List<Node>();

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
            FillNeightborSquares();
        }

        void SelectThreadDirection()
        {

        }

        void FillNeightborSquares()
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
                if(_battlefieldData.FindDirection(_currentNode.square, node.square, out var direction))
                {
                    threadDirection = direction;
                }
            }
        }

        void CheckTroopTargetingToNode(Troop troop, Node node)
        {
            if(node == _currentNode)
            {
                if (_battlefieldData.FindDirection(node.square, _currentNode.square, out var direction))
                {
                    threadDirection = direction;
                }
            }
        }
    }
}