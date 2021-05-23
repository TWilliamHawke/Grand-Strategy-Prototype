using UnitEditor;
using UnityEngine;

public class BuildingSelectionButtonEditor : BuildingSelectionButton
{
    [SerializeField] TemplateController _templateController;

    protected override IBuildController _buildController => _templateController;
}
