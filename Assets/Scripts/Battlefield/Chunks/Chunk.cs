using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlefield.Generator;
using UnityEngine.Events;

namespace Battlefield.Chunks
{
    public class Chunk : MonoBehaviour, IFrameController, IPounterController, IPathArrowController
    {
        static public event UnityAction<Chunk> OnPointerHide;

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
        ChunkRotation _chunkRotation;

        public Directions currentDirection => _innerArrowController.direction;

        void Awake()
        {
            _frameController = new FrameController(_frame, _rules);
            _innerArrowController = new InnerArrowController(_pointer, _centerPosition);
            _pathArrowController = new PathArrowController(_pathArrow);
            _chunkRotation = new ChunkRotation(_frameController, _selectedObjects, _innerArrowController);

            _battlefieldData.AddNode(this);
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void GenerateFrame(ChunkGenerator generator)
        {
            foreach(var framePart in _frame)
            {
                framePart.GenerateMesh(generator);
            }
        }

        public void UpdateFrameColors(int direction)
        {
            _frameController.UpdateFrameColors(direction);
        }

        public void HidePathArrow()
        {
            _pathArrowController.HidePathArrow();
        }

        public void RotatePathArrow(Directions direction)
        {
            _pathArrowController.RotatePathArrow(direction);
        }

        public void RotatePointer(Vector3 position)
        {
            int directionIndex = _innerArrowController.RotatePointer(position);
            _frameController.UpdateFrameColors(directionIndex);
        }

        public void SetDefaultFrameColor()
        {
            _frameController.SetDefaultFrameColor();
        }

        public void SetHoverColor()
        {
            _frameController.SetHoverColor();
        }

        public void HidePointer()
        {
            _innerArrowController.HidePointer();
            OnPointerHide?.Invoke(this);
        }

        public void ShowPointer()
        {
            _innerArrowController.ShowPointer();
        }


    }
}
