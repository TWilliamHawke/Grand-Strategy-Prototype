using UnityEngine;

public abstract class Equipment : ScriptableObject, IEquipment
{
    public string Name;
    public UnitNamePart unitNames;
    [SpritePreview(52, 52)]
    public Sprite sprite;
    public int goldCost;
    [SerializeField] Technology _requiredTechnology;

    public abstract string GetToolTipText();

    public bool isUnlocked => _requiredTechnology?.isResearched ?? true;
    public UnitNamePart UnitNames => unitNames;
    public Sprite Sprite => sprite;
    public string title => Name;

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

public interface IEquipment
{
    UnitNamePart UnitNames { get; }
    Sprite Sprite { get; }
    string title { get; }
}
