using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingSelectionButton : UIDataElement<Building>, IPointerClickHandler
{

    [SerializeField] Text _buildingName;
    [SerializeField] Image _buildingIcon;
    [SerializeField] TemplateController _templateController;

    Building _buildingData;



    public override string GetTooltipText()
    {
        return _buildingData.GetEffectsDescription();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideTooltip();
        _templateController.AddBuilding(_buildingData);
    }

    public override void UpdateData(Building data)
    {
        _buildingData = data;
        _buildingName.text = data.localizedName;
        _buildingIcon.sprite = data.icon;
    }

}
