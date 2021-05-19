using UnityEngine;
using UnityEngine.UI;

public class BuildingPreview : UIDataElement<Building>
{
    Building _buildingData;
    [SerializeField] Image _buildingIcon;

    public override string GetTooltipText()
    {
        var name = _buildingData.localizedName;
        var effect = _buildingData.GetEffectsDescription();

        return $"{name}\n{effect}";
    }

    public override void UpdateData(Building data)
    {
        _buildingData = data;
        _buildingIcon.sprite = data.icon;
    }
}
