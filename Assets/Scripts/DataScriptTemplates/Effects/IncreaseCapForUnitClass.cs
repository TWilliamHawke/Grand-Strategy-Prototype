using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "IncreaseClassCap", menuName = "Effects/IncreaseClassCap", order = 20)]
    public class IncreaseCapForUnitClass : Effect, IEffectChangeUnitClass
    {
        [SerializeField] UnitClass _unitClass;
        [SerializeField] int _unitCap;

        public UnitClass unitClass => _unitClass;
        public int unitCap => _unitCap;

        public override string GetText()
        {
            if(_unitCap >= 99)
            {
                return $"Unlock unlimited recruitment for {_unitClass.className.ToLower()}";
            }
            return $"increases cap for {_unitClass.className.ToLower()} by {_unitCap}";
        }

    }
}