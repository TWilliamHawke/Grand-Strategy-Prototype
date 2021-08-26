using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
using UnityEngine;

namespace Battlefield
{
    public class Troop : MonoBehaviour, ISelectable
    {

        [SerializeField] TroopStateIndicator _indicator;
        [SerializeField] MeshRenderer _selector;
        [SerializeField] BattlefieldData _battlefieldData;
        [SerializeField] bool _ownedByPlayer;
        [SerializeField] float _baseVisualSpeed = 1f;
        [SerializeField] float _rotationSpeed = 15f;

        //dynamic data
        float _visualSpeed = 1;
        bool _isSelected = false;
        Node _currentNode;
        Stack<Node> _path = new Stack<Node>();
        Directions _targetSquareDirection;

        public bool ownedByPlayer => _ownedByPlayer;
        public bool isSelected => _isSelected;

        //position data
        public Directions direction { get; set; }
        public Vector3 targetPosition { get; set; }
        public float rotationAngle { get; set; } = 0f;
        public float rotationDirection { get; set; } = 0f;
        public Directions nextTargetDirection => FindNextTargetDirection();
        public Stack<Node> path => _path;
        public Node currentNode => _currentNode;
        public Chunk chunk => _currentNode.chunk;

        public UnitsController enemy { get; set; }

        //cache
        UnitsController _unitsController;

        void Awake()
        {
            Chunk.OnPointerHide += SetFinalTargetDirection;
            _unitsController = GetComponent<UnitsController>();
            SetRotation(direction);

            targetPosition = transform.position;
        }

        void OnDestroy()
        {
            Chunk.OnPointerHide -= SetFinalTargetDirection;
        }

        void SpawnTroop()
        {
            //UNDONE
            //Find node!
            //_currentNode = 
        }

        void Update()
        {
            bool targetIsReached = targetPosition == null || targetPosition == transform.position;

            if (!targetIsReached)
            {
                MoveToTarget();
            }

            if (rotationAngle > Mathf.Epsilon)
            {
                RotateSquare();
            }
        }

        public Node FindCurrentNode()
        {
            _currentNode = _battlefieldData.FindNode(gameObject);
            return _currentNode;
        }

        public void SetRotation(Directions targetDirection)
        {
            direction = targetDirection;
            SetRotation((int)direction);
        }

        public void SetRotation(int direction)
        {
            transform.eulerAngles = new Vector3(0, direction * 45, 0);
        }

        public Directions FindNextTargetDirection()
        {
            if (path.Count == 0) return _targetSquareDirection;

            if (_battlefieldData.FindDirection(_currentNode, path.Peek(), out var pathDirection))
            {
                return pathDirection;
            }
            return direction;
        }

        public void TeleportTo(Node targetNode)
        {
            targetPosition = targetNode.chunk.transform.position;
            transform.position = targetNode.chunk.transform.position;
            SetNode(targetNode);
        }

        public void ChangeVisualSpeed(Timer timer)
        {
            float mult = timer.ticksPerSecond;
            _visualSpeed = _baseVisualSpeed * mult;
            _unitsController?.ChangeAnimationSpeed(mult);
        }

        public void Select()
        {
            _indicator.SetSelectedBackground();
            UpdateSquareBorders();
            ShowPath();
            _isSelected = true;
        }

        public void Deselect()
        {
            _indicator.SetDefaultBackground();
            _currentNode.chunk.SetDefaultFrameColor();
            _battlefieldData.HideAllInfo();
            _isSelected = false;
        }

        public void CreatePathTo(Node targetNode)
        {
            if (_currentNode == targetNode)
            {
                path.Clear();
                _unitsController.SetIsWalkValue(false);
                TeleportTo(targetNode);
            }
            else
            {
                var pathFinder = new PathFinder(_currentNode, targetNode, _battlefieldData);
                _path = pathFinder.GetPath();
                _unitsController.SetIsWalkValue(true);
            }

            _battlefieldData.HideAllInfo();
            ShowPath();
        }

        public void SetNode(Node nextNode)
        {
            _currentNode.chunk.SetDefaultFrameColor();
            _currentNode = nextNode;
            if (_isSelected)
            {
                UpdateSquareBorders();
            }
        }

        public void UpdateSquareBorders()
        {
            _currentNode.chunk.UpdateFrameColors((int)direction);
        }

        public void RestoreChunkFrame()
        {
            chunk.UpdateFrameColors((int)direction);
        }

        void SetFinalTargetDirection(IPounterController chunk)
        {
            if (_isSelected == false) return;
            _targetSquareDirection = chunk.currentDirection;
        }

        void RotateSquare()
        {
            float deltaAngle = _rotationSpeed * Time.deltaTime * _visualSpeed;
            if (rotationAngle < 1f)
            {
                deltaAngle = rotationAngle;
            }

            rotationAngle -= deltaAngle;
            float newAngle = transform.rotation.eulerAngles.y + (deltaAngle * rotationDirection);

            transform.eulerAngles = new Vector3(0, newAngle, 0);
        }

        void MoveToTarget()
        {
            float speed = _visualSpeed * Time.deltaTime;
            var nextPosition = Vector3.MoveTowards(transform.position, targetPosition, speed);
            transform.position = nextPosition;
        }

        void ShowPath()
        {
            Node currentNode = _currentNode;


            foreach (var node in path)
            {
                if (_battlefieldData.FindDirection(currentNode, node, out var direction))
                {
                    currentNode.chunk.RotatePathArrow(direction);
                }
                currentNode = node;
            }
        }
    }
}