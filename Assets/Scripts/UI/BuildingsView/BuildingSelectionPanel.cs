using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingSelectionPanel : UIPanelWithGrid<Building>
{
    [SerializeField] BuildingsListController _buildingsListController;

    protected override List<Building> _layoutElementsData => buildingsList;

    List<Building> buildingsList = new List<Building>();

    abstract protected List<Building> GetWasteBuildings();

    public void Show()
    {
        FillLayoutElementsList();
        if (_layoutElementsData.Count == 0) return;
        UpdateLayout();

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    protected void UpdateSelector()
    {
        FillLayoutElementsList();
        if (_layoutElementsData.Count == 0) return;
        UpdateLayout();
        Canvas.ForceUpdateCanvases();
    }



    protected void FillLayoutElementsList()
    {
        _layoutElementsData.Clear();
        var wasteBuildings = GetWasteBuildings();

        var possibleBuildings = _buildingsListController.FilterBuildings(BuildingSlots.castleAny);

        foreach (var building in possibleBuildings)
        {
            if (wasteBuildings.Contains(building)) continue;

            _layoutElementsData.Add(building);
        }

        if(_layoutElementsData.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }

}
