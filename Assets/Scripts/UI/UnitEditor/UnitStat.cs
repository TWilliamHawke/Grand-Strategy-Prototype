using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
    public class UnitStat : MonoBehaviour, INeedInit
    {
        [SerializeField] TemplateController _templateController;
        [Header("UI Elements")]
        [SerializeField] Text _attackValue;
        [SerializeField] Text _defenceValue;
        [SerializeField] Text _damageValue;
        [SerializeField] Text _healthValue;
        [SerializeField] Text _speedValue;
        [SerializeField] Text _chargeValue;

        private void OnDestroy()
        {
            _templateController.OnTemplateChange -= UpdateStat;
        }

        public void Init()
        {
            _templateController.OnTemplateChange += UpdateStat;

        }

        void UpdateStat(UnitTemplate template)
        {
            var stats = new UnitStats(template);

            _attackValue.text = stats.attack.ToString();
            _defenceValue.text = stats.defence.ToString();
            _damageValue.text = stats.damage.ToString();
            _healthValue.text = stats.health.ToString();
            _speedValue.text = stats.speed.ToString();
            _chargeValue.text = stats.charge.ToString();
        }
    }

}