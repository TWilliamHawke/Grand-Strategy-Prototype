using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
	public class TestActionWindow : MonoBehaviour, INeedInit
	{
		[SerializeField] SpyNetworkController _controller;
		[Header("UI Elements")]
	    [SerializeField] Text _actionName;
		[SerializeField] Text _actionChance;
		[SerializeField] Text _actionVisibility;
		[SerializeField] Text _actionCost;
		[SerializeField] Button _confirmButton;
		[SerializeField] Button _cancelButton;
		[SerializeField] Image _actionResultPanel;
		[SerializeField] Text _actionResultText;

		SpyAction _action;


		private void Awake() {
			_cancelButton.onClick.AddListener(Close);
			_confirmButton.onClick.AddListener(ExecuteAction);
		}

        public void Open(SpyAction action)
        {
			gameObject.SetActive(true);
			_action = action;
			_actionName.text = "Action: " + action.title;
			_actionChance.text = "Success chance: " + action.successChance.ToString() + "%";
			_actionVisibility.text = "Visibility: +" + action.visibility.ToString();
			_actionCost.text = "Cost: " + action.cost.ToString() + " points";
        }

        public void Init()
        {
            _controller.AddTestWindow(this);
        }

		public void ShowResultPanel(int result)
		{
			_actionResultPanel.gameObject.SetActive(true);
			var resultText = result < _action.successChance ? "success" : "fail";
			_actionResultText.text = $"Action chance: {_action.successChance}%\nRNG result: {result}.\n Action was {resultText}";
		}

		public void Close()
		{
			_actionResultPanel.gameObject.SetActive(false);
			gameObject.SetActive(false);
		}

		void ExecuteAction()
		{
			_controller.ExecuteSpyAction(_action);
		}

    }
}