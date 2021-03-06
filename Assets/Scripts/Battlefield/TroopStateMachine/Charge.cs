using UnityEngine;

namespace Battlefield
{
    public class Charge : Movement
    {
        protected override float _progressPerTick => 0.2f;
        public override Sprite stateIcon => stateConfig.chargeStateIcon;

        public Charge(Troop troopInfo, UnitsController unitsController) : base(troopInfo, unitsController)
        {
        }

        public override void OnEnter()
        {
            _unitsController.SetAnimatorValue("IsRun", true);
            _unitsController.MoveUnitsToChunkBack();
        }

        public override void OnExit()
        {
            _troopInfo.NormalizeUnitPositions();
            _unitsController.SetAnimatorValue("IsRun", false);
        }

    }
}