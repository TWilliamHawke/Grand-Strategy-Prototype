using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class NetworkUpgradeButton : UIElementWithTooltip, INeedInit
    {
        [SerializeField] SpyNetworkController _controller;
        [SerializeField] Button _button;

        int _unlockRequirements;

        public void Init()
        {
            _controller.OnSpyNetworkUpdate += CheckState;
            _controller.OnSpyNetworkLevelUp += ShowTooltip;
        }

        void OnDestroy()
        {
            _controller.OnSpyNetworkUpdate -= CheckState;
            _controller.OnSpyNetworkLevelUp -= ShowTooltip;
        }

        public override string GetTooltipText()
        {
            if (_controller.networkData.level >= _controller.levels.Count)
            {
                return "You reach maximum spy level";
            }
            else
            {
                int idx = _controller.networkData.level;
                int points = _controller.levels[idx].requiredSpyPoints;
                return $"You need {points} spy points to unlock next level";
            }
        }

        void CheckState()
        {
            _button.interactable = CheckConditions();
        }

        bool CheckConditions()
        {
            int level = _controller.networkData.level;
            if (_controller.levels.Count <= level) return false;

            if (_controller.networkData.spyPoints
                < _controller.levels[level].requiredSpyPoints) return false;

            return true;
        }
    }

}