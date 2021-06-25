using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prebattle
{

    public class PrebattleUnitCard : UIDataElement<UnitTemplate>
    {
        UnitTemplate _template;
        [SerializeField] Image unitIcon;

        public override string GetTooltipText()
        {
            return _template.fullName;
        }

        public override void UpdateData(UnitTemplate data)
        {
            _template = data;
            unitIcon.sprite = data.unitClass.defaultIcon;
        }

    }
}