using UnityEngine;

namespace Battlefield
{
    public class Movement : AbstractState
    {
        protected Troop _troopInfo;
        protected UnitsController _unitsController;
        override public Sprite stateIcon => stateConfig.movementStateIcon;

        protected virtual float _progressPerTick => 0.1f;

        public Movement(Troop troopInfo, UnitsController unitsController)
        {
            _troopInfo = troopInfo;
            _unitsController = unitsController;
        }

        public override void OnEnter()
        {
            _unitsController.SetIsWalkValue(true);
        }

        public override void OnExit()
        {
            _unitsController.SetIsWalkValue(false);
        }

        public override void Tick()
        {
            _progress += _progressPerTick;
            Vector3 nextSquarePosition = _troopInfo.path.Peek().square.transform.position;
            ChangeTroopsTargetPosition(nextSquarePosition);

            if (_troopInfo.transform.position == nextSquarePosition)
            {
                ChangeTargetNode();
            }
        }

        void ChangeTroopsTargetPosition(Vector3 nextSquarePosition)
        {
            Vector3 currentSquarePosition = _troopInfo.square.transform.position;

            var targetPosition = Vector3.Lerp(
                currentSquarePosition, nextSquarePosition, clampedProgress);

            _troopInfo.targetPosition = targetPosition;
        }

        void ChangeTargetNode()
        {
            var targetNode = _troopInfo.path.Pop();
            targetNode.EnterTroop(_troopInfo);
            _troopInfo.square.HidePathArrow();
            _troopInfo.SetSquare(targetNode.square);
            ResetProgress();
        }
    }
}