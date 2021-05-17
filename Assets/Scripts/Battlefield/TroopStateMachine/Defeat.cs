using UnityEngine;

namespace Battlefield
{

public class Defeat : AbstractState
{
    UnitsController _unitsController;
    TroopStatesAI _troopStates;
    override public Sprite stateIcon => stateConfig.fightStateIcon;


        public Defeat(UnitsController unitsController, TroopStatesAI troopStates)
        {
            _unitsController = unitsController;
            _troopStates = troopStates;
        }

        public override void Tick()
    {
        _unitsController.KillRandomUnit();
        _progress = 1f - (float)_unitsController.numOfUnits / 60;
        Debug.Log(_unitsController.numOfUnits);
    }

    public override void OnExit()
    {
        _troopStates.ToggleFightrogress(false);
    }

    public override void OnEnter()
    {
        _unitsController.SetAnimatorValue("IsFight", true);
        _troopStates.ToggleDisplay(false);
        _troopStates.ToggleFightrogress(true);
    }
}

}