using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlefield;

public class TimerPanel : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _increaseSpeedButton;
    [SerializeField] Button _decreaseSpeedButton;
    [SerializeField] Text _timeDisplay;
    [SerializeField] Text _speedDisplay;
    [SerializeField] Image _pauseMessage;

    [SerializeField] Timer _timer;

    void Awake()
    {
        EnablePauseUI();

        _timer.OnPause += EnablePauseUI;
        _timer.OnStart += DisablePauseUI;
        _timer.OnSpeedChange += UpdateSpeed;
        _timer.OnTimeChange += UpdateTimerDisplay;
    }

    void OnDestroy()
    {
        _timer.OnPause -= EnablePauseUI;
        _timer.OnStart -= DisablePauseUI;
        _timer.OnSpeedChange -= UpdateSpeed;
        _timer.OnTimeChange -= UpdateTimerDisplay;
    }

    void EnablePauseUI()
    {
        _playButton.interactable = true;
        _pauseButton.interactable = false;
        _pauseMessage.gameObject.SetActive(true);
    }

    void DisablePauseUI()
    {
        _playButton.interactable = false;
        _pauseButton.interactable = true;
        _pauseMessage.gameObject.SetActive(false);
    }

    void UpdateTimerDisplay()
    {
        _timeDisplay.text = _timer.GetTime();
    }

    void UpdateSpeed(Timer _)
    {
        _speedDisplay.text = $"{_timer.battlespeed + 1}x";
        UpdateSpeedButtons(_timer.battlespeed);
        UpdateTimerDisplay();
    }

    void UpdateSpeedButtons(int speed)
    {
        if (speed == 0) {
            _decreaseSpeedButton.interactable = false;
        } else {
            _decreaseSpeedButton.interactable = true;
        }

        if (speed >= _timer.maxSpeed) {
            _increaseSpeedButton.interactable = false;
        } else {
            _increaseSpeedButton.interactable = true;
        }
    }
}
