using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    public class TroopStatesPlayer : TroopStates
    {

        new void Awake()
        {
            base.Awake();

            var rotation = new Rotation(_troopInfo, _unitsController);
            var movement = new Movement(_troopInfo, _unitsController);
            var ready = new Ready();
            var charge = new Charge(_troopInfo, _unitsController);
            var fight = new Fight(_unitsController, this);
            var victory = new Victory(_unitsController);

            Go(movement, HasNewDirection, rotation);
            Go(movement, ReachTarget, ready);
            Go(ready, HasNewDirection, rotation);
            Go(ready, HasNewTarget, movement);
            Go(rotation, ReachDirectionAndHasTarget, movement);
            Go(rotation, ReachDirection, ready);
            Go(movement, EnemyInTargetNode, charge);
            Go(charge, ReachTarget, fight);
            Go(fight, WinFight, victory);
            Go(victory, UnitsOnPosition, ready);

            bool HasNewDirection() => _troopInfo.nextTargetDirection != _troopInfo.direction;
            bool ReachDirection() => _troopInfo.nextTargetDirection == _troopInfo.direction;
            bool EnemyInTargetNode() => _troopInfo.path.Peek().enemyInNode == true && ReachDirection();
            bool HasNewTarget() => _troopInfo.path.Count > 0;
            bool ReachTarget() => _troopInfo.path.Count == 0;
            bool ReachDirectionAndHasTarget() => ReachDirection() && HasNewTarget();
            //bool AttackEnemy() => _troopInfo.enemy != null && _troopInfo.enemy.numOfUnits > 0;
            bool WinFight() => _troopInfo.enemy?.numOfUnits == 0;
            bool UnitsOnPosition() => _unitsController.unitsOnPosition;

            _stateMachine.SetState(ready);

        }

    }
}