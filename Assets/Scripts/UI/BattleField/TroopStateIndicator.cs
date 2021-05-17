using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopStateIndicator : MonoBehaviour
{
    CameraController _camera;

    [SerializeField] Image _progressBar;
    [SerializeField] Image _stateIcon;
    [SerializeField] Image _stateBackground;
    [SerializeField] float _updateSpeedSecond = 0.2f;

    [Header("Sprites")]
    [SerializeField] Sprite _defaultBackground;
    [SerializeField] Sprite _selectedBackground;

    void Awake()
    {
        _camera = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }

    public void SetDefaultBackground()
    {
        _stateBackground.sprite = _defaultBackground;
    }

    public void SetSelectedBackground()
    {
        _stateBackground.sprite = _selectedBackground;
    }

    public void UpdateProgress(float progress)
    {
        _progressBar.fillAmount = progress;
    }

    public void ChangeStateIcon(Sprite sprite)
    {
        _stateIcon.sprite = sprite;
    }

    IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = _progressBar.fillAmount;
        float elapsed = 0f;

        while (elapsed < _updateSpeedSecond)
        {
            elapsed += Time.deltaTime;
            _progressBar.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSecond);
            yield return null;
        }

        _progressBar.fillAmount = pct;
    }



}
