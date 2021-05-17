using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentList", menuName = "Unit Editor/Equipment List", order = 49)]
public class EquipmentList : ScriptableObject
{
    public List<Equipment> equipmentList;
}
