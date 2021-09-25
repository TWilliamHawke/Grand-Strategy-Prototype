using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlobalMap.Diplomacy.UI
{
	public class FactionInfo : MonoBehaviour
	{
	    [SerializeField] Image _coatOfArms;
		[SerializeField] Text _factionName;
		[SerializeField] UnityEvent<FactionInfo> UpdatePanelHandler;

		private void Awake() {
			UpdatePanelHandler?.Invoke(this);
		}

		public void UpdateFactionInfo(FactionData data)
		{
			_coatOfArms.sprite = data.coatOfArms;
			_factionName.text = data.factionName;
		}
	}
}