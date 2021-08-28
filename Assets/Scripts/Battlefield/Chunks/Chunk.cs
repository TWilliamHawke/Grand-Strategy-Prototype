using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield.Generator;
using UnityEngine.Events;

namespace Battlefield.Chunks
{
    public class Chunk : MonoBehaviour
    {

        [SerializeField] BattlefieldData _battlefieldData;
        [SerializeField] MapConfig _mapConfig;
        [SerializeField] BattleRules _rules;
        [SerializeField] SelectedObjects _selectedObjects;

        [Header("Chunk visualization")]
        [SerializeField] ChunkArrow _pointer;
        [SerializeField] Transform _centerPosition;
        [SerializeField] ChunkArrow _pathArrow;
        [SerializeField] List<FramePartGenerator> _frame;

        //chunk logic
        FrameController _frameController;
        InnerArrowController _innerArrowController;
        PathArrowController _pathArrowController;

        public Directions currentDirection => _innerArrowController.direction;

        void Awake()
        {
            _frameController = new FrameController(_frame, _rules);
            _innerArrowController = new InnerArrowController(_pointer, _centerPosition, this);
            _pathArrowController = new PathArrowController(_pathArrow);

            _battlefieldData.AddNode(this);
        }

        void Update()
        {
            if (_selectedObjects.hoveredChunk == this && _selectedObjects.troop)
            {
                _innerArrowController.TryShowPointer(_selectedObjects.troop);
            }
        }

        public void GenerateFrame(ChunkGenerator generator)
        {
            foreach (var framePart in _frame)
            {
                framePart.GenerateMesh(generator);
            }
        }

        public void UpdateFrameColors(Directions direction)
        {
            _frameController.UpdateFrameColors((int)direction);
        }

        public void RestoreFrameColor()
        {
            if (_selectedObjects.hoveredChunk == this)
            {
                _frameController.SetHoverColor();
            }
            else
            {
                _frameController.SetDefaultFrameColor();
            }
        }

        public void SetDefaultFrameColor()
        {
            _frameController.SetDefaultFrameColor();
        }

        public void SetHoverColor()
        {
            _frameController.SetHoverColor();
        }

        public void HidePathArrow()
        {
            _pathArrowController.HidePathArrow();
        }

        public void RotatePathArrow(Directions direction)
        {
            _pathArrowController.RotatePathArrow(direction);
        }

    }

    public enum ChunkTypes
    {
        unwalkable,
        plane,
        slope
    }
}
