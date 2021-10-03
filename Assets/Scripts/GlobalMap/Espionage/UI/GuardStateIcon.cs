using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace GlobalMap.Espionage.UI
{
    public class GuardStateIcon : UIElementWithTooltip, INeedInit
    {
        [SerializeField] SpyNetworkController _controller;
        [SerializeField] int _statePointer;
        [SerializeField] Image _icon;

        string _tooltipText;
		StringBuilder _sb = new StringBuilder();



        public override string GetTooltipText()
        {
            return _tooltipText;
        }

        public void Init()
        {
            _controller.OnSpyNetworkUpdate += UpdateState;
        }

        private void OnDestroy()
        {
            _controller.OnSpyNetworkUpdate -= UpdateState;
        }

        void UpdateState()
        {
            GuardState state = _controller.GetGuardState(_statePointer);
            _icon.sprite = state.icon;
            _tooltipText = CreateTooltipText(state);
        }

        string CreateTooltipText(GuardState state)
        {
            _sb.Clear();
			int additionals = 0;

            _sb.AppendLine(state.title);
            _sb.AppendLine();

            if (state.actionCostPct != 0)
            {
                _sb.Append("Spy actions cost: ");
                AddPctValue(state.actionCostPct);
				additionals++;
            }

			if(state.actionsSuccessChance != 0)
			{
				_sb.Append("Actions success chance: ");
				AddPctValue(state.actionsSuccessChance);
				additionals++;
			}

			if(state.visibilityPerActionPct != 0)
			{
				_sb.Append("Visibility per action: ");
				AddPctValue(state.visibilityPerActionPct);
				additionals++;
			}

			if(additionals > 0)
			{
				_sb.AppendLine();
			}
            _sb.Append("Maximum visibility: ");
            _sb.AppendLine(state.visibilityCap.ToString());
            _sb.Append("VisibilityPerMonth: ");
            _sb.AppendLine(state.visibilityPerMonth.ToString());

            _sb.Remove(_sb.Length - 1, 1);

            return _sb.ToString();
        }

        void AddPctValue(float value)
        {
            if (value > 0)
            {
                _sb.Append("+");
            }
            _sb.Append(value);
            _sb.AppendLine("%");
        }
    }
}