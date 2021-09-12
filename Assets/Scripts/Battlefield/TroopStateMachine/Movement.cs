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
            _troopInfo.NormalizeUnitPositions();
            _unitsController.SetIsWalkValue(false);
        }

        public override void Tick()
        {
            _progress += _progressPerTick;
            Vector3 nextChunkPosition = _troopInfo.path.Peek().chunkCenter;
            ChangeTroopsTargetPosition(nextChunkPosition);
            //UNDONE
            _unitsController.NormalizeUnitsPosition();

            if (_troopInfo.transform.position == nextChunkPosition)
            {
                ChangeTargetNode();
            }
        }

        void ChangeTroopsTargetPosition(Vector3 nextChunkPosition)
        {
            Vector3 currentChunkPosition = _troopInfo.currentNode.chunkCenter;

            var targetPosition = Vector3.Lerp(
                currentChunkPosition, nextChunkPosition, clampedProgress);

            _troopInfo.targetPosition = targetPosition;
        }

        void ChangeTargetNode()
        {
            var targetNode = _troopInfo.path.Pop();
            targetNode.EnterTroop(_troopInfo);
            _troopInfo.chunk.HidePathArrow();
            _troopInfo.SetNode(targetNode);
            ResetProgress();
        }
    }
}