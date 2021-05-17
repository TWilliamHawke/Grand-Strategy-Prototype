using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
public class UnitClassInfo : MonoBehaviour
{
    [SerializeField] TemplateController _templateController;
    [SerializeField] Text _classNameText;
    [SerializeField] Text _weaponSkillText;
    [SerializeField] Text _wealthText;

    private void OnEnable() {
        _templateController.OnTemplateChange += UpdateInfo;
        UpdateInfo(_templateController.currentTemplate);
    }

    private void OnDisable() {
        _templateController.OnTemplateChange -= UpdateInfo;
    }

    public void UpdateInfo(UnitTemplate template)
    {
        var weaponSkill = template.unitClass.weaponSkill;
        _weaponSkillText.text = weaponSkill.ToString();
        
        int wealth = template.unitClass.wealth;
        int equipmentCost = template.GetEquipmentTotalCost();
        _wealthText.text = $"{equipmentCost}/{wealth}";

        string unitClass = template.unitClass.className;
        _classNameText.text = $"Class:{unitClass}";
    }

    


}

}
