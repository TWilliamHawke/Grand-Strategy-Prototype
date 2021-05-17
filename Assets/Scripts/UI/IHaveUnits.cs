using System.Collections.Generic;
using UnityEngine;

public interface IHaveUnits
{
    List<Unit> unitList { get; set; }
    void RemoveUnit(Unit unit);
    Transform transform { get; }
}