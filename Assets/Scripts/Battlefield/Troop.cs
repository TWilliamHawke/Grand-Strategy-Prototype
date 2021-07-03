using System.Collections;
using System.Collections.Generic;
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
        Square _square;
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
        public Square square => _square;
        public Node currentNode => _battlefieldData.FindNode(square);

        public UnitsController enemy { get; set; }

        //cache
        UnitsController _unitsController;

        void Awake()
        {
            Square.OnPointerHide += SetFinalTargetDirection;
            _unitsController = GetComponent<UnitsController>();
            SetRotation(direction);

            targetPosition = transform.position;
        }

        void OnDestroy()
        {
            Square.OnPointerHide -= SetFinalTargetDirection;    
        }

        void Start()
        {
            _square = _battlefieldData.FindSquare(this.gameObject);
            if (!_ownedByPlayer)
            {
                SetRotation(Directions.south);
                square.EnemyOnSquare = true;
            }
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

            if (_battlefieldData.FindDirection(square, path.Peek()?.square, out var pathDirection))
            {
                return pathDirection;
            }
            return direction;
        }

        public void TeleportTo(Square targetSquare)
        {
            targetPosition = square.transform.position;
            transform.position = square.transform.position;
            SetSquare(targetSquare);
        }

        public void ChangeVisualSpeed(Timer timer)
        {
            float mult = timer.ticksPerSecond;
            _visualSpeed = _baseVisualSpeed * mult;
            _unitsController.ChangeAnimationSpeed(mult);
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
            square.SetDefaultFrameColor();
            _battlefieldData.HideAllInfo();
            _isSelected = false;
        }

        public void CreatePathTo(Square targetSquare)
        {
            if (square == targetSquare)
            {
                path.Clear();
                _unitsController.SetIsWalkValue(false);
                TeleportTo(square);
            }
            else
            {
                var pathFinder = new PathFinder(square, targetSquare, _battlefieldData);
                _path = pathFinder.GetPath();
                _unitsController.SetIsWalkValue(true);
            }

            _battlefieldData.HideAllInfo();
            ShowPath();
        }

        public void SetSquare(Square nextSquare)
        {
            square.SetDefaultFrameColor();
            _square = nextSquare;
            if(_isSelected)
            {
                UpdateSquareBorders();
            }
        }

        public void UpdateSquareBorders()
        {
            _square.UpdateFrameColors(direction);
        }

        void SetFinalTargetDirection(Square square)
        {
            if(_isSelected == false) return;
            _targetSquareDirection = square.currentDirection;
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
            Square prevSquare = square;
            foreach (var node in path)
            {
                prevSquare.RotatePathArrow(node.square);
                prevSquare = node.square;
            }
        }
    }
}