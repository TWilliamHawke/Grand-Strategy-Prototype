using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield.Generator;


namespace Battlefield.Chunks
{
    public class FrameController
    {
        List<FramePartGenerator> _frame;
        BattleRules _rules;

        public FrameController(List<FramePartGenerator> frame, BattleRules battleRules)
        {
            _frame = frame;
            _rules = battleRules;
        }

        public void GenerateFrame(ChunkGenerator generator)
        {
            foreach (var framePart in _frame)
            {
                framePart.GenerateMesh(generator);
            }
        }

        public void UpdateFrameColors(int direction)
        {
            if (_frame.Count != 8)
            {
                Debug.Log("Frame list isn't full!");
                return;
            }

            for (int i = 0; i < 8; i++)
            {
                int colorIndex = i - direction;
                if (colorIndex < 0)
                {
                    colorIndex = 8 + i - direction;
                }

                _frame[i].UpdateColor(_rules.frameColors[colorIndex]);
            }
        }

        public void SetDefaultFrameColor()
        {
            SetFrameColor(_rules.defaultColor);
        }

        public void SetHoverColor()
        {
            SetFrameColor(_rules.hoverColor);
        }

        void SetFrameColor(Color color)
        {
            foreach (var framePart in _frame)
            {
                framePart.UpdateColor(color);
            }
        }

    }
}