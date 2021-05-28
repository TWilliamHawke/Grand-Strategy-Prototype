using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{

    public abstract class AbstractState : IState
    {
        public static StateConfig stateConfig
        {
            get => _stateConfig;
            set => _stateConfig = _stateConfig ?? value;
        }

        static StateConfig _stateConfig;

        public virtual Sprite stateIcon => _stateConfig?.defaultStateIcon;

        public float clampedProgress => Mathf.Clamp01(_progress);
        protected float _progress = 0f;

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Tick();

        protected void ResetProgress()
        {
            _progress = 0f;
        }
    }
}