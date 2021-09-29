using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
	public class NetworkScalePart : UIElementWithTooltip
	{
		string _tooltipText;
		[SerializeField] Image _background;
		[SerializeField] SpyNetworkController _netWorkController;

        public override string GetTooltipText()
        {
            return _tooltipText;
        }

		public void UpdateTooltipText(SpyNetworkLevel networkLevel)
		{

		}

		public void Unlock()
		{
			_background.color = _netWorkController.networkLevelsColor;
		}

	}
}