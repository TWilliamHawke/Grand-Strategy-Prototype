using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingNAme", menuName = "Other/Building")]
public class Building : ScriptableObject
{
    public string localizedName;
    public int goldCost;
    public BuildingSlots BuildingSlot;

    public List<Effect> effects = new List<Effect>();
}

public enum BuildingSlots
{
    castleMain,
    castlePort,
    castleAny,
}