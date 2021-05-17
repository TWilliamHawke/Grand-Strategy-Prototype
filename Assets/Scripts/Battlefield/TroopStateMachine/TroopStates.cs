using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    [RequireComponent(typeof(Troop))]
    [RequireComponent(typeof(UnitsController))]
    public abstract class TroopStates : MonoBehaviour
    {
        [SerializeField] protected Timer _timer;
        [SerializeField] protected StateConfig _stateConfig;
        [SerializeField] protected TroopStateIndicator _display;
        public TroopStateIndicator display => _display;

        protected StateMachine _stateMachine;
        protected Troop _troopInfo;
        protected UnitsController _unitsController;

        protected virtual void Awake()
        {
            _stateMachine = new StateMachine();
            AbstractState.stateConfig = _stateConfig;

            _troopInfo = GetComponent<Troop>();
            _unitsController = GetComponent<UnitsController>();

            _timer.OnTick += _stateMachine.Tick;
            _timer.OnSpeedChange += _troopInfo.ChangeVisualSpeed;

            _stateMachine.OnStateIconChange += _display.ChangeStateIcon;
            _stateMachine.OnStateProgressChange += _display.UpdateProgress;
        }

        protected virtual void OnDestroy()
        {
            _timer.OnTick -= _stateMachine.Tick;
            _timer.OnSpeedChange -= _troopInfo.ChangeVisualSpeed;
        }

        protected void At(IState to, IState from, Func<bool> condition) =>
            _stateMachine.AddTransition(from, to, condition);


        public void ToggleDisplay(bool state)
        {
            _display.gameObject.SetActive(state);
        }
    }

}