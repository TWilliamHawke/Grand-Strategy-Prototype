using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitEditor;
using Effects;

    public abstract class Equipment : ScriptableObject
    {
        public string Name;
        public UnitNamePart unitNames;
        public Sprite sprite;
        public int goldCost;

        void OnEnable()
        {
            if (unitNames == null)
            {
                Debug.LogError($"Equipment {name} doesn't have any unit names!");
            }
        }

        public abstract string GetToolTipText();

        public int CalculateCurrentCost(TemplateController templateController)
        {
            var effects = templateController.FindAllEffects<ChangeCostForEquipment>();
            var realCost = goldCost;

            foreach(var effect in effects)
            {
                if(effect.equipmentList.equipmentList.Contains(this))
                {
                    realCost *= effect.costDecresePct;
                    realCost /= 100;
                }
            }

            return realCost;
        }

        public bool ConvertTo<T>(out T item) where T : Equipment
        {
            if (this is T)
            {
                item = (T)this;
                return true;
            }
            else
            {
                Debug.LogError("Wrong Equipment type for" + Name);
                item = null;
                return false;
            }
        }


    }
