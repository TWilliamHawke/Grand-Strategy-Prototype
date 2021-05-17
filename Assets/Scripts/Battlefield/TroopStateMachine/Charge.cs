using UnityEngine;

namespace Battlefield
{
    public class Charge : Movement
    {
        //float _progressPerTick = 0.2f;

        public Charge(Troop troopInfo, UnitsController unitsController) : base(troopInfo, unitsController)
        {
        }

        override public Sprite stateIcon => stateConfig.chargeStateIcon;

        public override void OnEnter()
        {
            _unitsController.SetAnimatorValue("IsRun", true);
            _unitsController.ChangeUnitsPosition(UnitsPosition.back);
        }

        public override void OnExit()
        {
            _unitsController.SetAnimatorValue("IsRun", false);
        }

    }
}