using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

            if(Input.GetMouseButtonUp(0))
            {
                _isPressed = false;
                if(_oldSliderValue == _slider.value) return;


                _unitClassInfo.FinaliizeWeaponSkills();
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
        }

        public void Init()
        {
            _slider.onValueChanged.AddListener(ReadSliderValue);
            _closeButton.onClick.AddListener(Hide);
        }

        //used in editor
        public void PressOnSlider()
        {
            _isPressed = true;
            _oldSliderValue = _slider.value;
        }

        void ReadSliderValue(float value)
        {
            float percentValue = value / _slider.maxValue;
            _unitClassInfo.UpdateWeaponSkill(percentValue);
        }

    }
}