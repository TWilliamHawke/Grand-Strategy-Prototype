using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public Unit(UnitTemplate template, SettlementData settlement)
    {
        unitTemplate = template;
        nativeSettlement = settlement;
    }

    public UnitTemplate unitTemplate;
    public int currentSize;
    int maxSize => unitTemplate.unitClass.unitSize;
    SettlementData nativeSettlement;

}
