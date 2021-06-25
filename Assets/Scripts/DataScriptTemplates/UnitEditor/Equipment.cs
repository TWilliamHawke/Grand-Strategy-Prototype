using UnityEngine;

    public abstract class Equipment : ScriptableObject
    {
        public string Name;
        public UnitNamePart unitNames;
        public Sprite sprite;
        public int goldCost;
        [SerializeField] Technology _requiredTechnology;

        public abstract string GetToolTipText();

        public bool isUnlocked => _requiredTechnology?.isResearched ?? true;

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
