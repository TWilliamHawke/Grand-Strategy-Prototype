using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class InnerSlopeGenerator : CornerGenerator
    {
        public InnerSlopeGenerator(MapConfig mapConfig) : base(mapConfig)
        {
        }

        protected override float GetHeight(float distance)
        {
            return (1 + _mapConfig.slopeCurve.Evaluate(distance)) * _mapConfig.heightPerLevel;
        }

        protected override bool SelectCorner(out Vector3 lowestCorner)
        {
            float sumHeights = 0;
            lowestCorner = _corners[0];

            foreach (var corner in _corners)
            {
                sumHeights += corner.y;

                if (corner.y < lowestCorner.y)
                {
                    lowestCorner = corner;
                }
            }

            if (sumHeights % 4 == 3)
            {
                return true;
            }

            return false;
        }
    }
}