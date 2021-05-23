using System.Collections.Generic;
using UnityEngine;

public interface IHaveUnits
{
    List<Unit> unitList { get; }
    void RemoveUnit(Unit unit);
    Vector3 position { get; }
}