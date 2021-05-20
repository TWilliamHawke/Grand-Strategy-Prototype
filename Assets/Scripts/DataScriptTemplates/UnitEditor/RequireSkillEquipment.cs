using UnitEditor;
using Effects;
using UnityEngine;

public abstract class RequireSkillEquipment : Equipment
{
    public int requiredSkill;

    public int CalculateRealSkill(TemplateController _templateController)
    {
        int realSkill = requiredSkill;

        var effects = _templateController.FindAllEffects<ChangeRequirementForWeapon>();

        foreach(var effect in effects)
        {
            if(effect.equipmentList.equipmentList.Contains(this))
            {
                realSkill -= effect.requiredSkillDecrease;
            }
        }

        return Mathf.Max(realSkill, 0);
    }

}
