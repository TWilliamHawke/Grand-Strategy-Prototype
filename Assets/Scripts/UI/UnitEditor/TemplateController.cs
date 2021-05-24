using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace UnitEditor
{

    [CreateAssetMenu(fileName = "TemplateController", menuName = "Unit Editor/TemplateController", order = 100)]
    public class TemplateController : ScriptableObject, IBuildController
    {
        public event UnityAction<UnitTemplate> OnTemplateChange;
        public event UnityAction<UnitTemplate> OnTemplateSave;
        public event UnityAction OnTemplateSelection;
        public event UnityAction OnBuildingAdded;

        [SerializeField] UnitTemplate _emptyTemplate;
        [SerializeField] TechnologiesController _techController;

        public UnitTemplate defaultTemplate { get; private set; }
        public UnitTemplate currentTemplate { get; private set; }
        public int realwealth => currentTemplate.unitClass.CalculateRealWealth(this);

        void OnEnable()
        {
            currentTemplate = Instantiate(_emptyTemplate);
            UpdateTemplate();
        }

        public void SaveTemplate()
        {
            OnTemplateSave?.Invoke(currentTemplate);
        }

        public void SelectTemplate(UnitTemplate template)
        {
            defaultTemplate = template;
            currentTemplate = Instantiate(defaultTemplate);
            OnTemplateSelection?.Invoke();
            UpdateTemplate();
        }

        public void ResetTemplate()
        {
            currentTemplate = Instantiate(defaultTemplate);
            UpdateTemplate();
        }

        public void UpdateTemplate()
        {
            OnTemplateChange?.Invoke(currentTemplate);
        }

        public void AddBuilding(Building building)
        {
            currentTemplate.AddRequiredBuildings(building);
            OnBuildingAdded?.Invoke();
        }

        public List<T> FindAllEffects<T>()
        {
            var effects = currentTemplate.FindBuildingsEffects();

            foreach (var tech in _techController.researchedTechnologies)
            {
                effects.AddRange(tech.effects);
            }

            return effects.OfType<T>().ToList<T>();
        }


    }

}