using UnityEngine;
using UnityEngine.Events;

namespace UnitEditor
{

    [CreateAssetMenu(fileName = "TemplateController", menuName = "Unit Editor/TemplateController", order = 100)]
    public class TemplateController : ScriptableObject, IBuilder
    {
        //events
        public event UnityAction<UnitTemplate> OnTemplateChange;
        public event UnityAction OnTemplateSelection;
        public event UnityAction OnBuildingsChange;
        public event UnityAction OnWeaponSkillChange;

        [SerializeField] UnitTemplate _emptyTemplate;
        [SerializeField] TechnologiesController _techController;
        [SerializeField] UnitsListController _unitList;

        UnitTemplate _defaultTemplate;
        UnitTemplate _currentTemplate;
        TemplateEffectsCalculator _effectsCalculator;

        //getters
        public UnitTemplate defaultTemplate => _defaultTemplate;
        public UnitTemplate currentTemplate => _currentTemplate;
        public int realwealth => _effectsCalculator.CalculateRealWealth(_currentTemplate);

        void OnEnable()
        {
            _effectsCalculator = new TemplateEffectsCalculator(_techController, this);
            _currentTemplate = _emptyTemplate.Clone();
            UpdateTemplate();
        }

        public void SaveTemplate()
        {
            _currentTemplate.Save(_unitList);
        }

        public void SelectTemplate(UnitTemplate template)
        {
            _defaultTemplate = template;
            _currentTemplate = _defaultTemplate.Clone();

            OnTemplateSelection?.Invoke();
            UpdateTemplate();
        }

        public void ResetTemplate()
        {
            _currentTemplate = _defaultTemplate.Clone();
            UpdateTemplate();
        }

        public void UpdateTemplate()
        {
            OnTemplateChange?.Invoke(_currentTemplate);
        }

        public void UpdateWeaponSkills(int meleeSkill)
        {
            _currentTemplate.meleeSkill = meleeSkill;
            OnWeaponSkillChange?.Invoke();
        }

        public void AddBuilding(Building building)
        {
            _currentTemplate.AddRequiredBuildings(building);
            OnBuildingsChange?.Invoke();
        }

        public void RemoveBuilding(Building building)
        {
            bool isSuccess = _currentTemplate.TryRemoveRequiredBuiding(building);

            if(isSuccess)
            {
                OnBuildingsChange?.Invoke();
            }
        }

        public int FindRealItemCost(Equipment item)
        {
            return _effectsCalculator.CalculateItemCost(item);
        }

        public int FindRealSkillForItem(Equipment item)
        {
            return _effectsCalculator.CalculateRealSkill(item);
        }

    }

}