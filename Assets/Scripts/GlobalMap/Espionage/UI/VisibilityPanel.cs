using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class VisibilityPanel : MonoBehaviour, INeedInit
    {
        [SerializeField] SpyNetworkController _controller;
        [Header("UI Elements")]
        [SerializeField] Text _visibilityText;
        [SerializeField] Image _bar;

        public void Init()
        {
            _controller.OnSpyNetworkUpdate += UpdatePanel;
        }

        private void OnDestroy()
        {
            _controller.OnSpyNetworkUpdate -= UpdatePanel;
        }

        void UpdatePanel()
        {
			_bar.fillAmount = _controller.networkData.visibility / _controller.maxVisibility;
            _visibilityText.text = $"{_controller.networkData.visibility}/{_controller.maxVisibility}";
        }


    }
}