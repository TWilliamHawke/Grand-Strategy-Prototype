using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitEditor;

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
            return goldCost;
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
