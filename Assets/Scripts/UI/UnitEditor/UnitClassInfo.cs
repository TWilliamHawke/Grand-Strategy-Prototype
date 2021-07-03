using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitClassInfo : MonoBehaviour, INeedInit
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] WeaponSkillSlider _slider;
        
        [Header("UI Elements")]
        [SerializeField] Text _classNameText;
        [SerializeField] Text _meleeSkillText;
        [SerializeField] Text _rangeSkillText;
        [SerializeField] Text _wealthText;
        [SerializeField] Button _weaponSkillsButton;

        int _equipmentCost;
        int _classWealth;
        int _meleeSkill;
        int _rangeSkill;

        void OnDisable()
        {
            _slider.Hide();
        }

        private void OnDestroy()
        {
            _templateController.OnTemplateChange -= UpdateClassInfo;
            _templateController.OnBuildingsChange -= SetWealthAndSkills;
            _itemSlotController.OnEquipmentCostChange -= SetEquipmentCost;
        }

        public void Init()
        {
            _weaponSkillsButton.onClick.AddListener(_slider.Show);
            _templateController.OnTemplateChange += UpdateClassInfo;
            _templateController.OnBuildingsChange += SetWealthAndSkills;
            _itemSlotController.OnEquipmentCostChange += SetEquipmentCost;
        }

        public void UpdateClassInfo(UnitTemplate template)
        {
            SetWealthAndSkills();

            string unitClass = template.unitClass.className;
            _classNameText.text = $"Class:{unitClass}";
        }

        public void UpdateWeaponSkill(float sliderValue)
        {
            //0 means melleeSkill = max, rangeSkill = 0
            _rangeSkill = Mathf.RoundToInt(_itemSlotController.classSkill * sliderValue);
            _meleeSkill = _itemSlotController.classSkill - _rangeSkill;

            _meleeSkillText.text = _meleeSkill.ToString();
            _rangeSkillText.text = _rangeSkill.ToString();

        }

        public void FinaliizeWeaponSkills()
        {
            _templateController.UpdateWeaponSkills(_meleeSkill);
        }

        void SetWealthAndSkills()
        {
            var _meleeSkill = _templateController.currentTemplate.meleeSkill;
            var _rangeSkill = _templateController.currentTemplate.rangeSkill;
            _meleeSkillText.text = _meleeSkill.ToString();
            _rangeSkillText.text = _rangeSkill.ToString();

            _slider.UpdateSliderValue(_meleeSkill, _rangeSkill);

            _classWealth = _templateController.realwealth;
            UpdateWealthText();
        }

        void SetEquipmentCost(int cost)
        {
            _equipmentCost = cost;
            UpdateWealthText();
        }

        void UpdateWealthText()
        {
            _wealthText.text = $"{_equipmentCost}/{_classWealth}";
        }

    }

}
