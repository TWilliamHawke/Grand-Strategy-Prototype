using System.Collections;
using System.Collections.Generic;
using UnitEditor;
using UnityEngine;

public class BuildingSelectorEditor : BuildingSelectionPanel
{
    [SerializeField] TemplateController _templateController;

    protected override List<Building> GetWasteBuildings()
    {
        return _templateController.currentTemplate.requiredBuildings;
    }

    void Awake()
    {
        _templateController.OnBuildingAdded += UpdateSelector;
    }

    void OnDestroy()
    {
        _templateController.OnBuildingAdded -= UpdateSelector;
    }



}
