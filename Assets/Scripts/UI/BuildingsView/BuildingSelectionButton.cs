using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BuildingSelectionButton : UIDataElement<Building>, IPointerClickHandler
{

    [SerializeField] Text _buildingName;
    [SerializeField] Image _buildingIcon;

    Building _buildingData;

    protected abstract IBuildController _buildController { get; }

    public override string GetTooltipText()
    {
        return _buildingData.GetEffectsDescription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideTooltip();
        _buildController?.AddBuilding(_buildingData);
    }

    public override void UpdateData(Building data)
    {
        _buildingData = data;
        _buildingName.text = data.localizedName;
        _buildingIcon.sprite = data.icon;
    }

}
