using UnitEditor;
using UnityEngine;

public class BuildingSelectionButtonEditor : BuildingSelectionButton
{
    [SerializeField] TemplateController _templateController;

    protected override IBuilder _buildController => _templateController;
}
