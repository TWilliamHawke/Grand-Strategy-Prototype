using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] Timer _timer;

        float _tickInterval = 1f;

        Coroutine _timerCoroutine;

        void Awake()
        {
            _timer.ResetTimer();

            _timer.OnStart += StartTimer;
            _timer.OnPause += StopTimer;
            _timer.OnSpeedChange += UpdateInterval;
        }

        void OnDestroy()
        {
            _timer.OnStart -= StartTimer;
            _timer.OnPause -= StopTimer;
            _timer.OnSpeedChange -= UpdateInterval;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _timer.ToggleTimer();
            }

            if(Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                _timer.IncreaseSpeed();
            }
            if(Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                _timer.DecreaseSpeed();
            }
        }

        void StartTimer()
        {
            _timerCoroutine = StartCoroutine(Tick());
        }

        void StopTimer()
        {
            if(_timerCoroutine == null) return;
            StopCoroutine(_timerCoroutine);
        }

        void UpdateInterval(Timer _)
        {
            _tickInterval = _timer.tickInterval;
        }

        IEnumerator Tick()
        {
            while (true)
            {
                yield return new WaitForSeconds(_tickInterval);
                _timer.Tick();
            }
        }
    }
}