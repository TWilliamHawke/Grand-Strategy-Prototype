using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class SpyPointsPanel : MonoBehaviour, INeedInit
    {
        [SerializeField] SpyNetworkController _networkController;
        [Header("UI Elements")]
        [SerializeField] Text _spyPoints;
        [SerializeField] Text _spyPointsGrouth;

        public void Init()
        {
            _networkController.OnSpyNetworkUpdate += UpdateText;
        }

        private void OnDestroy()
        {
            _networkController.OnSpyNetworkUpdate -= UpdateText;
        }

        void UpdateText()
        {
			_spyPoints.text = _networkController.networkData.spyPoints.ToString();
			_spyPointsGrouth.text = "+" + _networkController.spyPointsPerMonth.ToString();
        }
    }
}