using UnityEngine;
using UnityEngine.UI;

public class BuildingPreview : UIDataElement<Building>
{
    Building _buildingData;
    [SerializeField] Image _buildingIcon;

    public override string GetTooltipText()
    {
        return _buildingData.GetFullDescription();
    }

    public override void UpdateData(Building data)
    {
        _buildingData = data;
        _buildingIcon.sprite = data.icon;
    }
}
