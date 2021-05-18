using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitEditor;

namespace Effects
{
    [CreateAssetMenu(fileName = "IncreaseClassWealth", menuName = "Effects/IncreaseClassWealth", order = 20)]
    public class IncreaseClassWealth : Effect, IEffectChangeUnitClass
    {
        [SerializeField] UnitClass _unitClass;
        [SerializeField] int addWealth;

        public UnitClass unitClass => _unitClass;

        public override string GetText()
        {
            return $"Increases wealth for {_unitClass.className.ToLower()} by {addWealth}";
        }
    }
}