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
        [SerializeField] Text _classNameText;
        [SerializeField] Text _weaponSkillText;
        [SerializeField] Text _wealthText;

        UnitClass _unitClass;
        int _equipmentCost;
        int _classWealth;

        private void OnDestroy()
        {
            _templateController.OnTemplateChange -= UpdateClassInfo;
            _templateController.OnBuildingsChange -= SetWealth;
            _itemSlotController.OnEquipmentCostChange -= SetEquipmentCost;
        }

        public void Init()
        {
            _templateController.OnTemplateChange += UpdateClassInfo;
            _templateController.OnBuildingsChange += SetWealth;
            _itemSlotController.OnEquipmentCostChange += SetEquipmentCost;
        }

        public void UpdateClassInfo(UnitTemplate template)
        {
            _unitClass = template.unitClass;

            var weaponSkill = template.unitClass.weaponSkill;
            _weaponSkillText.text = weaponSkill.ToString();

            SetWealth();

            string unitClass = template.unitClass.className;
            _classNameText.text = $"Class:{unitClass}";
        }

        void SetWealth()
        {
            _classWealth = _templateController.realwealth;
            UpdateText();
        }

        void SetEquipmentCost(int cost)
        {
            _equipmentCost = cost;
            UpdateText();
        }

        void UpdateText()
        {
            _wealthText.text = $"{_equipmentCost}/{_classWealth}";
        }
    }

}
