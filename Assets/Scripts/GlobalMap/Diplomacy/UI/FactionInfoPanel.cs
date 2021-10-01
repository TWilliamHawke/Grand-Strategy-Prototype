using System.Collections;
using System.Collections.Generic;
using GlobalMap.Factions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GlobalMap.Diplomacy.UI
{
    public class FactionInfoPanel : MonoBehaviour, INeedInit
    {
        [SerializeField] Image _coatOfArms;
        [SerializeField] Text _factionName;
        [SerializeField] DiplomacyController _diplomacyController;
        [SerializeField] UnityEvent UpdatePanelHandler;

        public void Init()
        {
            _diplomacyController.OnFactionSelect += CallEvent;
        }

        void OnDestroy()
        {
            _diplomacyController.OnFactionSelect -= CallEvent;
        }

        public void UpdateFactionInfo(Faction faction)
        {
            _coatOfArms.sprite = faction.factionData.coatOfArms;
            _factionName.text = faction.factionData.factionName;
        }

        void CallEvent(Faction _)
        {
            UpdatePanelHandler?.Invoke();
        }
    }
}