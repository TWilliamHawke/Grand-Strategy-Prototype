using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHaveUnits
{
    string localizedName { get; }
    List<Unit> unitList { get; }
    void AddUnit(UnitTemplate template);
    void RemoveUnit(Unit unit);
    void Defeat(Faction winner);
    Transform transform { get; }
}