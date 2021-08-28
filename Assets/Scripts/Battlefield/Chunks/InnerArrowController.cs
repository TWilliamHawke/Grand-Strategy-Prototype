using System.Collections;
using System.Collections.Generic;
using Battlefield.Generator;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield.Chunks
{
    public class InnerArrowController
    {
        static public event UnityAction<Chunk> OnPointerHide;
        static Coroutine _delayCoroutine;
        static InnerArrowController _lastController;

        float _showDelay = 0.3f;
        bool _isShow;

        ChunkArrow _pointer;
        Transform _centerPosition;
        Directions _currentDirection;
        Chunk _chunk;

        public Directions direction => _currentDirection;

        public InnerArrowController(ChunkArrow pointer, Transform centerPosition, Chunk chunk)
        {
            _pointer = pointer;
            _centerPosition = centerPosition;
            _chunk = chunk;
        }

        public void TryShowPointer(Troop troop)
        {
            TogglePointer(troop);
            TryRotatePointer();
        }

        void TryRotatePointer()
        {
            if (!Input.GetMouseButton(1)) return;
            if (!_isShow) return;

            if (Raycasts.GetPosition(out var position))
            {
                var direction = RotatePointer(position);
                _chunk.UpdateFrameColors(direction);
            }
        }

        Directions RotatePointer(Vector3 position)
        {
            _centerPosition.LookAt(position);
            int directionIndex = Mathf.RoundToInt(_centerPosition.eulerAngles.y / 45);
            var newDirection = (Directions)directionIndex;

            if (_currentDirection != newDirection)
            {
                float angleY = directionIndex * 45;
                //_pointer.transform.eulerAngles = new Vector3(0, angleY, 0);
                _pointer.transform.rotation = Quaternion.Euler(0, angleY, 0);
                _pointer.UpdateShape();
                _currentDirection = newDirection;
            }

            return newDirection;
        }

        void TogglePointer(Troop troop)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _lastController = this;
                _delayCoroutine = _chunk.StartCoroutine(ShowAfterDelay(troop));
            }

            if (Input.GetMouseButtonUp(1))
            {
                _lastController?.HidePointer();
                _chunk.RestoreFrameColor();

                _chunk.StopCoroutine(_delayCoroutine);
                _isShow = false;

                troop.UpdateChunkBorders();
            }
        }

        void HidePointer()
        {
            _pointer.gameObject.SetActive(false);
            OnPointerHide?.Invoke(_chunk);
        }

        IEnumerator ShowAfterDelay(Troop troop)
        {
            yield return new WaitForSeconds(_showDelay);
            _isShow = true;
            _pointer.gameObject.SetActive(true);
            TryRotatePointer();
            troop.chunk.SetHoverColor();
        }

    }
}