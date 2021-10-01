using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalMap.Espionage.UI
{
    public class SpyActionButton : UIDataElement<SpyAction>
    {
        [SerializeField] SpyNetworkController _controller;
        [Header("UI Elements")]
        [SerializeField] Button _button;
        [SerializeField] Text _actionName;
        [SerializeField] Text _successChance;
        [SerializeField] Text _actionCost;
        [SerializeField] Text _visibilityText;

        SpyAction _action;

        void Awake()
        {
            _button.onClick.AddListener(OpenActionWindow);
        }

        public override string GetTooltipText()
        {
            return _action.title;
        }

        public override void UpdateData(SpyAction data)
        {
            _action = data;
			_actionName.text = _action.title;
			UpdateActionValues();
        }

        void OpenActionWindow()
        {
            _controller.OpenWindow(_action);
        }

		void UpdateActionValues()
        {
            _successChance.text = _action.successChance.ToString() + "%";
            _visibilityText.text = "+" + _action.visibility.ToString();
            _actionCost.text = _action.cost.ToString();

            if (NoeEnoughSpyPoints())
            {
                _button.interactable = false;
                _actionCost.color = Color.red;
            }
            else
            {
                _button.interactable = true;
                _actionCost.color = Color.black;
            }
        }

        private bool NoeEnoughSpyPoints()
        {
            return _action.cost > _controller.networkData.spyPoints;
        }
    }
}