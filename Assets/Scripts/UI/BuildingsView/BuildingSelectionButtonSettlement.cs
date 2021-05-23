
public class BuildingSelectionButtonSettlement : BuildingSelectionButton
{
    public static SettlementData settlementData { private get; set; }

    protected override IBuildController _buildController => settlementData;
}
