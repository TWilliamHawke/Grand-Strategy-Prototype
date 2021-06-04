
public class BuildingSelectionButtonSettlement : BuildingSelectionButton
{
    public static Settlement settlement { private get; set; }

    protected override IBuilder _buildController => settlement;
}
