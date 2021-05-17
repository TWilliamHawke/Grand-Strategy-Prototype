using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "BattleRules", menuName = "Battlefield/Battle Rules")]
    public class BattleRules : ScriptableObject
    {
        public Color frontColor = Color.green;
        public Color flankColor = Color.yellow;
        public Color rearColor = Color.red;
        public Color defaultColor = Color.blue;
        public Color hoverColor = Color.yellow;

        List<Color> _frameColors = new List<Color>();
        public List<Color> frameColors => _frameColors;

        void OnEnable()
        {
            // frame has 8 elements
            _frameColors = new List<Color>()
        {
            frontColor,
            frontColor,
            flankColor,
            flankColor,
            rearColor,
            rearColor,
            flankColor,
            flankColor
        };
        }
    }
}