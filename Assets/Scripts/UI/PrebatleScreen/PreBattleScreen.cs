using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prebattle
{

    public class PreBattleScreen : MonoBehaviour, INeedInit
    {
        [SerializeField] SkirmishController _skirmishController;
        [Header("UI Elements")]
        [SerializeField] PrebattleUnitList _attackerForce;
        [SerializeField] PrebattleUnitList _defenderForce;
        [SerializeField] Button _attackButton;
        [SerializeField] Button _retreatButton;
        [SerializeField] Image _winMessage;
        [SerializeField] Button _okButton;

        void OnDestroy()
        {
            _skirmishController.OnConfrontationStart -= ShowAndUpdate;
        }

        public void Init()
        {
            _skirmishController.OnConfrontationStart += ShowAndUpdate;
            _retreatButton.onClick.AddListener(RetreatHandler);
            _attackButton.onClick.AddListener(ShowWinMessage);
            _okButton.onClick.AddListener(HideWinScreen);
        }

        void RetreatHandler()
        {
            gameObject.SetActive(false);
            _skirmishController.Retreat();
        }

        void ShowAndUpdate(Army attacker, IHaveUnits defender)
        {
            _attackerForce.UpdateForceData(attacker);
            _defenderForce.UpdateForceData(defender);
            gameObject.SetActive(true);
        }

        void ShowWinMessage()
        {
            _winMessage.gameObject.SetActive(true);
            _attackButton.interactable = false;
            _retreatButton.interactable = false;
        }

        void HideWinScreen()
        {
            _attackButton.interactable = true;
            _retreatButton.interactable = true;
            _winMessage.gameObject.SetActive(false);

            gameObject.SetActive(false);
            _skirmishController.WinBattle();
        }

    }

}