using UnityEngine;

namespace Battlefield
{

    public class Rotation : AbstractState
    {
        protected Troop _troopInfo;
        override public Sprite stateIcon => stateConfig.rotationStateIcon;

        UnitsController _unitsController;
        float _maxProgress = 0;
        float _currentProgress = 0;

        public Rotation(Troop troopInfo, UnitsController unitsController)
        {
            _troopInfo = troopInfo;
            _unitsController = unitsController;
        }

        public override void OnEnter()
        {
            ResetProgress();
            _unitsController.SetIsWalkValue(true);
            int deltaDirection = FindTargetDirection() - _troopInfo.direction;

            if (deltaDirection > 4)
            {
                deltaDirection -= 8;
            }
            if (deltaDirection < -4)
            {
                deltaDirection += 8;
            }

            _troopInfo.rotationDirection = Mathf.Sign(deltaDirection);
            _currentProgress = 0;
            _maxProgress = Mathf.Abs(deltaDirection) * 45;
        }

        public override void OnExit()
        {
            _unitsController.SetIsWalkValue(false);
        }

        public override void Tick()
        {
            if (_currentProgress < _maxProgress)
            {
                _currentProgress += 15;
                _progress = _currentProgress / _maxProgress;
                _troopInfo.rotationAngle += 15;
            }

            if (_troopInfo.rotationAngle < 1f)
            {
                _troopInfo.direction = FindTargetDirection();
            }
        }

        protected virtual Directions FindTargetDirection()
        {
            return _troopInfo.FindNextTargetDirection();
        }
    }
}