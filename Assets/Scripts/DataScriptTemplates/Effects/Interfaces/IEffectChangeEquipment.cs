namespace Effects
{
    public interface IEffectChangeEquipment
    {
        EquipmentList equipmentList { get; }
        string GetText();
    }
}