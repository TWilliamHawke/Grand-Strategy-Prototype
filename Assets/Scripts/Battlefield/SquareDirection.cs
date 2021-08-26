using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
using UnityEngine;

namespace Battlefield
{
    public class SquareDirection : MonoBehaviour
    {
        [SerializeField] SelectedObjects _selectedObjects;
        [Range(0, 0.5f)]
        [SerializeField] float _showDelay = 0.1f;

        Chunk _currentSquare;
        Coroutine _delayCoroutine;

        bool _isShow;

        void Update()
        {
            if (_selectedObjects.hoveredChunk)
            {
                TogglePointer();
                RotatePointer();
            }
        }

        void RotatePointer()
        {
            if (!Input.GetMouseButton(1)) return;
            if (!_selectedObjects.troop || !_isShow) return;

            if (Raycasts.GetPosition(out var position))
            {
                _selectedObjects.hoveredChunk.RotatePointer(position);
            }
        }

        void TogglePointer()
        {
            if (!_selectedObjects.troop) return;

            if (Input.GetMouseButtonDown(1))
            {
                _currentSquare = _selectedObjects.hoveredChunk;
                _delayCoroutine = StartCoroutine(ShowAfterDelay());
            }

            if (Input.GetMouseButtonUp(1))
            {
                _currentSquare.HidePointer();
                if (_currentSquare == _selectedObjects.hoveredChunk)
                {
                    _currentSquare.SetHoverColor();
                }
                else
                {
                    _currentSquare.SetDefaultFrameColor();
                }

                StopCoroutine(_delayCoroutine);
                _isShow = false;

                _selectedObjects.troop.RestoreChunkFrame();
            }
        }

        IEnumerator ShowAfterDelay()
        {
            yield return new WaitForSeconds(_showDelay);
            _isShow = true;
            _currentSquare.ShowPointer();
            RotatePointer();
            _selectedObjects.troop.chunk.SetHoverColor();
        }
    }
}