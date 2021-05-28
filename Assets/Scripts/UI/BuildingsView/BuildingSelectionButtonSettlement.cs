
public class BuildingSelectionButtonSettlement : BuildingSelectionButton
{
    public static SettlementData settlementData { private get; set; }

    protected override IBuilder _buildController => settlementData;
}
