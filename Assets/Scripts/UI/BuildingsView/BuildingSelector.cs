using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;

public class BuildingSelector : UIPanelWithGrid<Building>
{
    [SerializeField] BuildingsListController _buildingsListController;
    [SerializeField] TemplateController _templateController;

    void Awake()
    {
        Show();
        _templateController.OnBuildingAdded += UpdateSelector;
    }

    void OnDestroy()
    {
        _templateController.OnBuildingAdded -= UpdateSelector;
    }

    void UpdateSelector()
    {
        FillLayoutElementsList();
        if (_layoutElementsData.Count == 0) return;
        UpdateGrid();
        Canvas.ForceUpdateCanvases();
    }

    public void Show()
    {
        FillLayoutElementsList();
        if (_layoutElementsData.Count == 0) return;
        UpdateGrid();

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    protected override void FillLayoutElementsList()
    {
        _layoutElementsData.Clear();
        var wasteBuildings = _templateController.currentTemplate.requiredBuildings;

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

    protected override void PlusButtonListener()
    {
    }
}
