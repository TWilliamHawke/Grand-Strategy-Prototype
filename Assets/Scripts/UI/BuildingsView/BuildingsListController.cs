using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsListController", menuName = "Core Game/Buildings List Controller")]
public class BuildingsListController : ScriptableObject
{
    [SerializeField] List<Building> _allBuildings;

    public List<Building> FilterBuildings(BuildingSlots slot)
    {
        var buildingsList = new List<Building>();

        foreach(var building in _allBuildings)
        {
            if(building.BuildingSlot == slot)
            {
                buildingsList.Add(building);
            }
        }

        return buildingsList;
    }
}
