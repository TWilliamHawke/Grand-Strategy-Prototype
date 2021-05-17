using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{

    public class TroopStatesAI : TroopStates
    {
        EnemyDetector _enemyDetector;
        [SerializeField] FightProgress _fightProgressBar;

        public FightProgress fightProgressBar => _fightProgressBar;

        new void Awake()
        {
            base.Awake();

            _enemyDetector = GetComponent<EnemyDetector>();

            var ready = new Ready();
            var rotation = new RotationToThread(_troopInfo, _unitsController, _enemyDetector);
            var defeat = new Defeat(_unitsController, this);
            var dead = new Dead();

            At(defeat, ready, IsUnderAttack());
            At(dead, defeat, TroopIsDefeated());
            At(rotation, ready, ThreadToFlank());
            At(ready, rotation, FlanksIsDefended());

            Func<bool> IsUnderAttack() => () => _enemyDetector.IsAttacked;
            Func<bool> ThreadToFlank() => () => _enemyDetector.threadDirection != _troopInfo.direction;
            Func<bool> FlanksIsDefended() => () => _enemyDetector.threadDirection == _troopInfo.direction;
            Func<bool> TroopIsDefeated() => () => _unitsController.numOfUnits == 0;

            _stateMachine.SetState(ready);

            _stateMachine.OnStateProgressChange += _fightProgressBar.UpdateProgress;

        }


        public void ToggleFightrogress(bool state)
        {
            _fightProgressBar.gameObject.SetActive(state);
        }
    }

}