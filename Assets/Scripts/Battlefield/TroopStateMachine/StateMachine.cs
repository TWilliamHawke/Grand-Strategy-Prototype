using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{

    public class StateMachine
    {
        IState _currentState;
        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        List<Transition> _currentTransitions = new List<Transition>();
        List<Transition> _anyTransitions = new List<Transition>();
        static List<Transition> _emptyTransitions = new List<Transition>();

        public event UnityAction<Sprite> OnStateIconChange;
        public event UnityAction<float> OnStateProgressChange;

        public void Tick()
        {
            var transition = GetTransition();

            if (transition != null)
            {
                SetState(transition.To);
            }

            _currentState.Tick();
            OnStateProgressChange?.Invoke(_currentState.clampedProgress);
        }

        public void SetState(IState state)
        {
            if (_currentState == state) return;

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
            {
                _currentTransitions = _emptyTransitions;
            }

            _currentState.OnEnter();
            OnStateIconChange?.Invoke(_currentState.stateIcon);
            OnStateProgressChange?.Invoke(0f);
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (!_transitions.TryGetValue(from.GetType(), out var transitions))
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                Condition = condition;
                To = to;
            }
        }

        Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition()) return transition;
            }

            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
            }

            return null;
        }
    }


    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
        Sprite stateIcon { get; }
        float clampedProgress { get; }
    }
}