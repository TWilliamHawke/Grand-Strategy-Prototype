using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHaveUnits
{
    List<Unit> unitList { get; }
    void AddUnit(UnitTemplate template);
    void RemoveUnit(Unit unit);
    void Defeat();
    Vector3 position { get; }
}