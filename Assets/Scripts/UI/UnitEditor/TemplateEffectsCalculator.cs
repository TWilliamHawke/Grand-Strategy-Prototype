using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;

namespace UnitEditor
{

    public class TemplateEffectsCalculator
    {
        TechnologiesController _techController;
        TemplateController _templateController;

        public TemplateEffectsCalculator(TechnologiesController techController,
                                         TemplateController templateController)
        {
            _techController = techController;
            _templateController = templateController;
        }

        public int CalculateItemCost(Equipment item)
        {
            var effects = FindAllEffects<ChangeCostForEquipment>();
            var currentCost = item.goldCost;

            foreach (var effect in effects)
            {
                if (effect.AffectsItem(item))
                {
                    //all values are int, dont use "effect.costDecresePct/100" !!
                    currentCost *= effect.costDecresePct;
                    currentCost /= 100;
                }
            }

            return currentCost;
        }

        public int CalculateRealSkill(Equipment item)
        {
            if (!(item is RequireSkillEquipment)) return 0;

            int skill = (item as RequireSkillEquipment).requiredSkill;

            var effects = FindAllEffects<ChangeRequirementForWeapon>();

            foreach (var effect in effects)
            {
                if (effect.AffectsItem(item))
                {
                    skill -= effect.requiredSkillDecrease;
                }
            }

            return Mathf.Max(skill, 0);
        }

        public int CalculateRealWealth(UnitTemplate template)
        {
            int realwealth = template.unitClass.wealth;

            var effects = FindAllEffects<IncreaseClassWealth>();

            foreach (var effect in effects)
            {
                if (effect.unitClass != template.unitClass) continue;

                realwealth += effect.addWealth;
            }

            return realwealth;
        }

        IEnumerable<T> FindAllEffects<T>()
        {
            var effects = _templateController.currentTemplate.FindBuildingsEffects();

            foreach (var tech in _techController.researchedTechnologies)
            {
                effects.AddRange(tech.effects);
            }

            return effects.OfType<T>();
        }

    }
}