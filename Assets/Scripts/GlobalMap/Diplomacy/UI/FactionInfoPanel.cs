using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlobalMap.Diplomacy.UI
{
    public class FactionInfoPanel : MonoBehaviour
    {
        [SerializeField] Image _coatOfArms;
        [SerializeField] Text _factionName;
        [SerializeField] UnityEvent UpdatePanelHandler;

        void OnEnable()
        {
            UpdatePanelHandler?.Invoke();
        }

        public void UpdateFactionInfo(FactionData data)
        {
            _coatOfArms.sprite = data.coatOfArms;
            _factionName.text = data.factionName;
        }
    }
}