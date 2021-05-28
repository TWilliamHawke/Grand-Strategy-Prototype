using UnityEngine;

namespace Battlefield
{
    public class Ready : AbstractState
    {
        override public Sprite stateIcon => stateConfig.defaultStateIcon;


        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void Tick()
        {
        }
    }
}