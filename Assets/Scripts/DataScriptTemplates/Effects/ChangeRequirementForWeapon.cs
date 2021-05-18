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

        public override string GetText()
        {
            var weaponGroup = _equipmentList.listName.ToLower();
            return $"Decrease skill for {weaponGroup} by {_requiredSkillDecrease}%";
        }
    }
}