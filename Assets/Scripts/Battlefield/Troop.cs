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
        public bool ownedByPlayer => _ownedByPlayer;
        [SerializeField] float _baseVisualSpeed = 1;
        [SerializeField] float _rotationSpeed = 15;

        float _visualSpeed = 1;

        //position data
        public Square square { get; set; }
        public Directions direction { get; set; }
        public Stack<Node> path { get; private set; } = new Stack<Node>();
        public Vector3 targetPosition { get; set; }
        public Directions nextTargetDirection => FindNextTargetDirection();
        public float rotationAngle { get; set; } = 0;
        public float rotationDirection { get; set; } = 0;
        public Node currentNode => _battlefieldData.FindNode(square);

        public UnitsController enemy { get; set; }

        //cache
        UnitsController _unitsController;

        private void Awake()
        {
            _unitsController = GetComponent<UnitsController>();
            SetRotation(direction);

            targetPosition = transform.position;
        }

        void Start()
        {
            square = _battlefieldData.FindSquare(this.gameObject);
            if(!_ownedByPlayer)
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

            if(rotationAngle > Mathf.Epsilon)
            {
                float deltaAngle = _rotationSpeed * Time.deltaTime * _visualSpeed;
                if(rotationAngle < 1f)
                {
                    deltaAngle = rotationAngle;
                }

                rotationAngle -= deltaAngle;
                float newAngle = transform.rotation.eulerAngles.y + (deltaAngle * rotationDirection);

                transform.eulerAngles = new Vector3(0, newAngle, 0);
            }
        }

        public void SetRotation(Directions targetDirection)
        {
            direction = targetDirection;
            SetRotation((int)direction);
        }

        public Directions FindNextTargetDirection()
        {
            if(path.Count == 0) return direction;

            if(_battlefieldData.FindDirection(square, path.Peek()?.square, out var pathDirection))
            {
                return pathDirection;
            }
            return direction;
        }

        public void SetRotation(int direction)
        {
            transform.eulerAngles = new Vector3(0, direction * 45, 0);
        }

        public void TeleportTo(Square targetSquare)
        {
            targetPosition = square.transform.position;
            transform.position = square.transform.position;
            square = targetSquare;
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
            square.UpdateFrameColors(direction);
            ShowPath();
        }

        public void Deselect()
        {
            _indicator.SetDefaultBackground();
            square.SetDefaultFrameColor();
            _battlefieldData.HideAllInfo();
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
                path = pathFinder.GetPath();
                _unitsController.SetIsWalkValue(true);
            }

            _battlefieldData.HideAllInfo();
            ShowPath();

        }

        private void MoveToTarget()
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