using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "Timer", menuName = "Battlefield/Timer")]
    public class Timer : ScriptableObject
    {
        int _battleSpeed = 0;
        int _ticksFromStart = 0;
        public int battlespeed => _battleSpeed;

        [SerializeField] int _startHour = 6;
        [SerializeField] int _startYear = 650;

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

        int[] _months = new int[] {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        

        public void Tick()
        {
            _ticksFromStart++;
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
            int hours = _startHour + _ticksFromStart / 60;
            string minutes = AddZeroToStart(_ticksFromStart % 60);
            return $"{hours}:{minutes}";
        }

        public string GetDate()
        {
            var year = _startYear + _ticksFromStart / 365;
            var day = _ticksFromStart % 365;
            var month = 1;

            for (int i = 1; i < _months.Length; i++)
            {
                if(day < _months[i]) break;
                day -= _months[i];
                month++;
            }

            return $"{AddZeroToStart(day + 1)}/{AddZeroToStart(month)}/{year}";
        }

        public void ResetTimer()
        {
            _ticksFromStart = 0;
            _battleSpeed = 0;
            OnSpeedChange?.Invoke(this);
            OnTimeChange?.Invoke();
        }

        string AddZeroToStart(int time)
        {
            return time > 9 ? time.ToString() : "0" + time.ToString();
        }
    }
}