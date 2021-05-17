using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
public class UnitStat : MonoBehaviour
{
    [SerializeField] TemplateController _templateController;
    [SerializeField] Text _attackValue;
    [SerializeField] Text _defenceValue;
    [SerializeField] Text _damageValue;
    [SerializeField] Text _healthValue;
    [SerializeField] Text _speedValue;
    [SerializeField] Text _chargeValue;

    private void OnEnable() {
        _templateController.OnTemplateChange += UpdateStat;
        UpdateStat(_templateController.currentTemplate);
    }

    private void OnDisable() {
        _templateController.OnTemplateChange -= UpdateStat;
    }

    void UpdateStat(UnitTemplate template)
    {
        _attackValue.text = template.attack.ToString();
        _defenceValue.text = template.defence.ToString();
        _damageValue.text = template.damage.ToString();
        _healthValue.text = template.health.ToString();
        _speedValue.text = template.speed.ToString();
        _chargeValue.text = template.charge.ToString();
    }
}

}