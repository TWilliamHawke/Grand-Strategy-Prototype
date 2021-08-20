using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield.Generator
{
    public class OuterSlopeGenerator : CornerGenerator
    {
        public OuterSlopeGenerator(MapConfig mapConfig) : base(mapConfig)
        {
        }

        protected override float GetHeight(float distance)
        {
            return (2 - _mapConfig.slopeCurve.Evaluate(distance)) * _mapConfig.heightPerLevel;
        }

        protected override bool SelectCorner(out Vector3 hightestCorner)
        {
            float sumHeights = 0;
            hightestCorner = _corners[0];

            foreach (var corner in _corners)
            {
                sumHeights += corner.y;

                if (corner.y > hightestCorner.y)
                {
                    hightestCorner = corner;
                }
            }

            if (sumHeights % 4 == 1)
            {
                return true;
            }

            return false;
        }
    }
}