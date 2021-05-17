using UnityEngine;

namespace Battlefield
{

public class Fight : AbstractState
{
    UnitsController _unitsController;
    TroopStates _troopStates;
    
    override public Sprite stateIcon => stateConfig.fightStateIcon;


        public Fight(UnitsController unitsController, TroopStates troopStates)
        {
            _unitsController = unitsController;
            _troopStates = troopStates;
        }

        public override void OnEnter()
    {
        _unitsController.SetAnimatorValue("IsFight", true);
        _troopStates.ToggleDisplay(false);
    }

    public override void OnExit()
    {
        _unitsController.SetAnimatorValue("IsFight", false);
        _troopStates.ToggleDisplay(true);
    }

    public override void Tick()
    {
        
    }

}

}