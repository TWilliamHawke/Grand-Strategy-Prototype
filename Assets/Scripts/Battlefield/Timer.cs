using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "Timer", menuName = "Battlefield/Timer")]
    public class Timer : ScriptableObject
    {
        int _hours = 6;
        int _minutes = 0;
        int _battleSpeed = 0;
        public int battlespeed => _battleSpeed;

        [SerializeField] int _startHour = 6;

        [SerializeField] List<float> _tickIntervals = new List<float>();

        public float ticksPerSecond => 1/_tickIntervals[_battleSpeed];
        public float tickInterval => _tickIntervals[_battleSpeed];
        public int maxSpeed => _tickIntervals.Count - 1;

        bool _isSuspended = true;


        public event UnityAction OnTick;
        public event UnityAction OnPause;
        public event UnityAction OnStart;
        public event UnityAction<Timer> OnSpeedChange;
        public event UnityAction OnTimeChange;

        public void Tick()
        {
            _minutes++;
            if (_minutes >= 60)
            {
                _minutes -= 60;
                _hours++;
            }

            OnTimeChange?.Invoke();
            OnTick?.Invoke();
        }

        public void ToggleTimer()
        {
            if(_isSuspended)
            {
                StartTimer();
            }
            else
            {
                SuspendTimer();
            }
        }

        public void SuspendTimer()
        {
            _isSuspended = true;
            OnPause?.Invoke();
        }

        public void StartTimer()
        {
            _isSuspended = false;
            OnStart?.Invoke();
        }

        public void IncreaseSpeed()
        {
            if (_battleSpeed < _tickIntervals.Count - 1)
            {
                _battleSpeed++;
                OnSpeedChange?.Invoke(this);
            }
        }

        public void DecreaseSpeed()
        {
            if (_battleSpeed > 0)
            {
                _battleSpeed--;
                OnSpeedChange?.Invoke(this);
            }
        }

        public string GetTime()
        {
            var minutes = _minutes > 9 ? _minutes.ToString() : "0" + _minutes.ToString();
            return $"{_hours}:{minutes}";
        }

        public void ResetTimer()
        {
            _hours = _startHour;
            _minutes = 0;
            _battleSpeed = 0;
            OnSpeedChange?.Invoke(this);
            OnTimeChange?.Invoke();
        }
    }
}