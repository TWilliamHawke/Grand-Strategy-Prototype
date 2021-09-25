using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Diplomacy.UI
{
	public class DiplomacyScreen : UIScreen
	{
	    [SerializeField] DiplomacyController diplomacyController;

		//used as  unityAction
		public void SetPlayerFactionData(FactionInfo infoPanel)
		{
			infoPanel.UpdateFactionInfo(diplomacyController.playerFaction);
		}

		//used as  unityAction
		public void SetTargetFactionData(FactionInfo infoPanel)
		{
			infoPanel.UpdateFactionInfo(diplomacyController.targetFaction);
		}
	}
}