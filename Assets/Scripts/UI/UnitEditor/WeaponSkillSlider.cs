using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{

    public class WeaponSkillSlider : MonoBehaviour, INeedInit
    {
        [SerializeField] Slider _slider;
        [SerializeField] UnitClassInfo _unitClassInfo;
        [SerializeField] Button _closeButton;

        bool _isPressed = false;
        float _oldSliderValue = 0f;

        void Update()
        {
            if (!_isPressed) return;

            if (Input.GetMouseButtonUp(0))
            {
                _isPressed = false;
                if (_oldSliderValue == _slider.value) return;


                _unitClassInfo.FinaliizeWeaponSkills();
                _oldSliderValue = _slider.value;
            }
        }

        public void UpdateSliderValue(int meleeSkill, int rangeSkill)
        {
            int totalSkill = meleeSkill + rangeSkill;
            _slider.maxValue = totalSkill / 5;
            _slider.value = rangeSkill / totalSkill * _slider.maxValue;
        }


        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _slider.value = _oldSliderValue;
        }

        public void Init()
        {
            _slider.onValueChanged.AddListener(UpdateWeaponSkill);
            _closeButton.onClick.AddListener(Hide);
        }

        //used in editor
        public void PressOnSlider()
        {
            _isPressed = true;
            _oldSliderValue = _slider.value;
        }

        void UpdateWeaponSkill(float value)
        {
            float percentValue = value / _slider.maxValue;
            _unitClassInfo.UpdateWeaponSkill(percentValue);
        }

    }
}