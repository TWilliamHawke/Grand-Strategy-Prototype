using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Effects;

namespace UnitEditor
{
    public class UnitClassInfo : MonoBehaviour
    {
        [SerializeField] TemplateController _templateController;
        [SerializeField] Text _classNameText;
        [SerializeField] Text _weaponSkillText;
        [SerializeField] Text _wealthText;

        UnitClass _unitClass;

        private void OnEnable()
        {
            _templateController.OnTemplateChange += UpdateInfo;
            _templateController.OnBuildingAdded += UpdateWealth;
            UpdateInfo(_templateController.currentTemplate);
        }

        private void OnDisable()
        {
            _templateController.OnTemplateChange -= UpdateInfo;
            _templateController.OnBuildingAdded -= UpdateWealth;
        }

        public void UpdateInfo(UnitTemplate template)
        {
            _unitClass = template.unitClass;

            var weaponSkill = template.unitClass.weaponSkill;
            _weaponSkillText.text = weaponSkill.ToString();

            UpdateWealth();

            string unitClass = template.unitClass.className;
            _classNameText.text = $"Class:{unitClass}";
        }

        private void UpdateWealth()
        {
            int wealth = _unitClass.wealth;

            var effects = _templateController.FindAllEffects<IncreaseClassWealth>();

            foreach (var effect in effects)
            {
                wealth += effect.addWealth;
            }

            int equipmentCost = _templateController.equipmentCost;
            _wealthText.text = $"{equipmentCost}/{wealth}";
        }

    }

}
