namespace Effects
{
    public interface IEffectChangeEquipment
    {
        string GetText();
        bool AffectsItem(Equipment item);
    }
}