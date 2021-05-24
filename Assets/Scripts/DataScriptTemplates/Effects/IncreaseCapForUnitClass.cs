using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "IncreaseClassCap", menuName = "Effects/IncreaseClassCap", order = 20)]
    public class IncreaseCapForUnitClass : Effect, IEffectChangeUnitClass
    {
        [SerializeField] UnitClass _unitClass;
        [SerializeField] int _unitCap;

        public UnitClass unitClass => _unitClass;

        public override string GetText()
        {
            return $"increases cap for {_unitClass.className.ToLower()} by {_unitCap}";
        }

    }
}