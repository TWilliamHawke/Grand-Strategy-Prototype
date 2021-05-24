using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "UnlockClass", menuName = "Effects/Unlock Class", order = 20)]
    public class UnlockClass : Effect, IEffectChangeUnitClass
    {
        [SerializeField] UnitClass _unitClass;


        public UnitClass unitClass => _unitClass;

        public override string GetText()
        {
            return $"Unlock recruitment: {_unitClass.className}";
        }
    }
}