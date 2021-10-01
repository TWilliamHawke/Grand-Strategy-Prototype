using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class NetworkUpgradeButton : MonoBehaviour, INeedInit
    {
        [SerializeField] SpyNetworkController _controller;
        [SerializeField] Button _button;

        int _unlockRequirements;

        public void Init()
        {
            _controller.OnSpyNetworkUpdate += CheckState;
        }

        void OnDestroy()
        {
            _controller.OnSpyNetworkUpdate -= CheckState;
        }

        void CheckState()
        {
            _button.interactable = CheckConditions();
        }

		bool CheckConditions()
		{
			if(_controller.levels.Count >= _controller.networkData.level) return true;
            int level = _controller.networkData.level;

            if(_controller.networkData.spyPoints
                >= _controller.levels[level].requiredSpyPoints) return true;

			return false;
		}
    }

}