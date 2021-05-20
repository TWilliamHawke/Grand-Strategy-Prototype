using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace UnitEditor
{
    [CreateAssetMenu(fileName = "TemplateController", menuName = "Unit Editor/TemplateController", order = 100)]
    public class TemplateController : ScriptableObject
    {
        public event UnityAction<UnitTemplate> OnTemplateChange;
        public event UnityAction<UnitTemplate> OnTemplateSave;
        public event UnityAction OnBuildingAdded;

        [SerializeField] UnitTemplate _emptyTemplate;
        [SerializeField] TechnologiesController _techController;

        public UnitTemplate defaultTemplate { get; private set; }
        public UnitTemplate currentTemplate { get; private set; }
        public int equipmentCost => currentTemplate.GetEquipmentTotalCost();
        public int realwealth => currentTemplate.unitClass.CalculateRealWealth(this);

        public List<UnitTemplate> defaultTemplates;

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
            currentTemplate.requiredBuildings.Add(building);
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

        public void AddPrimaryWeapon(Equipment equipment)
        {
            if (ConvertEquipment<Weapon>(equipment, out var weapon))
            {
                currentTemplate.primaryWeapon = weapon;
                UpdateTemplate();
            }
        }

        public void AddSecondaryWeapon(Equipment equipment)
        {
            if (ConvertEquipment<Weapon>(equipment, out var weapon))
            {
                currentTemplate.secondaryWeapon = weapon;
                UpdateTemplate();
            }
        }

        public void AddShield(Equipment equipment)
        {
            if (ConvertEquipment<Shield>(equipment, out var shield))
            {
                currentTemplate.shield = shield;
                UpdateTemplate();
            }
        }

        public void AddArmor(Equipment equipment)
        {
            if (ConvertEquipment<ArmourInfo>(equipment, out var armour))
            {
                currentTemplate.armour = armour;
                UpdateTemplate();
            }
        }

        public void AddMount(Equipment equipment)
        {
            if (ConvertEquipment<Mount>(equipment, out var mount))
            {
                currentTemplate.mount = mount;
                UpdateTemplate();
            }
        }

        bool ConvertEquipment<T>(Equipment equipment, out T item) where T : Equipment
        {
            if (equipment is T)
            {
                item = (T)equipment;
                return true;
            }
            else
            {
                ReportWrongItem(equipment);
                item = null;
                return false;
            }
        }

        void ReportWrongItem(Equipment equipment)
        {
            throw new System.Exception("Wrong item in list: " + equipment.Name);
        }

    }

}