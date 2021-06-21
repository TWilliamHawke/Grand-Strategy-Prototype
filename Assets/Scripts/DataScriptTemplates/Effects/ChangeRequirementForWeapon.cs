using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "DecreaseSkill", menuName = "Effects/Decrease Skill For Weapons")]
    public class ChangeRequirementForWeapon : Effect, IEffectChangeEquipment
    {
        [SerializeField] EquipmentList _equipmentList;
        [SerializeField] int _requiredSkillDecrease;

        public EquipmentList equipmentList => _equipmentList;
        public int requiredSkillDecrease => _requiredSkillDecrease;

        public override string GetText()
        {
            var weaponGroup = _equipmentList.listName.ToLower();
            return $"Decrease required skill for {weaponGroup} by {_requiredSkillDecrease}";
        }

        public bool AffectsItem(Equipment item)
        {
            return _equipmentList.equipmentList.Contains(item);
        }
    }
}