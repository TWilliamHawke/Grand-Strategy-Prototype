using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            var sb = new StringBuilder();

            sb.AppendLine(_action.title);
            if(NotEnoughSpyPoints())
            {
                sb.Append("<color=red>Not enough spy points, wait for next month(s)</color>");
            }

            return sb.ToString();
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
            _successChance.text = Mathf.Min(_action.successChance, 100).ToString() + "%";
            _visibilityText.text = "+" + _action.visibility.ToString();
            _actionCost.text = _action.cost.ToString();

            if (NotEnoughSpyPoints())
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

        private bool NotEnoughSpyPoints()
        {
            return _action.cost > _controller.networkData.spyPoints;
        }
    }
}