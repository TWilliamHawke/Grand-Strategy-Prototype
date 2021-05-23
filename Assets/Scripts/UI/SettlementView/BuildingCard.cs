using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCard : UIDataElement<Building>
{
    [SerializeField] Image _buldingIcon;

    Building _buildingData;

    public override string GetTooltipText()
    {
        return _buildingData.GetFullDescription();
    }

    public override void UpdateData(Building data)
    {
        throw new System.NotImplementedException();
    }
}
