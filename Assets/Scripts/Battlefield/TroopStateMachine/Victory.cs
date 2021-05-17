using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{

    public class Victory : AbstractState
    {
        UnitsController _unitsController;
        override public Sprite stateIcon => stateConfig.movementStateIcon;

        public Victory(UnitsController unitsController)
        {
            _unitsController = unitsController;
        }

        public override void OnEnter()
        {
            _unitsController.SetIsWalkValue(true);
            _unitsController.ChangeUnitsPosition(UnitsPosition.center);
        }

        public override void OnExit()
        {
            _unitsController.SetIsWalkValue(false);
        }

        public override void Tick()
        {
        }
    }

}