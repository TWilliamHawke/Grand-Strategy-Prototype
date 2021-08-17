using System.Collections.Generic;
using Effects;

public interface ITemplate: IRequireBuildings
{
    PartialName nameParts { get; }
    bool canNotEdit { get; }
    bool FindEquipment<T>(EquipmentSlots slot, out T equipment) where T : Equipment;
    void AddEquipment(EquipmentSlots slot, Equipment equipment);
}

public interface IRequireBuildings
{
    List<Building> requiredBuildings { get; }
    void AddRequiredBuildings(Building building);
    bool TryRemoveRequiredBuiding(Building building);
    List<Effect> FindBuildingsEffects();
}
