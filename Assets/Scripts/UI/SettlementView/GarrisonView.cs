using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarrisonView : UnitsView
{
    public Vector3 settlementPosition => _unitsOwner.transform.position;

    public List<Unit> GetSelectedUnits()
    {
        var unitList = new List<Unit>();

        foreach(var unitcard in selectedCards)
        {
            unitList.Add(unitcard.unit);
        }

        return unitList;
    }

    public bool AnyCardSelected() => selectedCards.Count != 0;


}
