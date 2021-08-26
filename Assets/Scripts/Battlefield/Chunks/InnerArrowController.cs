using System.Collections;
using System.Collections.Generic;
using Battlefield.Generator;
using UnityEngine;

namespace Battlefield.Chunks
{
    public class InnerArrowController
    {
        [SerializeField] ChunkArrow _pointer;
        [SerializeField] Transform _centerPosition;
        private Directions _currentDirection;

		public Directions direction => _currentDirection;

        public InnerArrowController(ChunkArrow pointer, Transform centerPosition)
        {
            _pointer = pointer;
            _centerPosition = centerPosition;
        }

        public int RotatePointer(Vector3 position)
        {
            _centerPosition.LookAt(position);
            int directionIndex = Mathf.RoundToInt(_centerPosition.eulerAngles.y / 45);
            var newDirection = (Directions)directionIndex;

            if (_currentDirection != newDirection)
            {
                float angleY = directionIndex * 45;
                //_pointer.transform.eulerAngles = new Vector3(0, angleY, 0);
                _pointer.transform.rotation = Quaternion.Euler(0, angleY, 0);
                _currentDirection = newDirection;
            }

            return directionIndex;
        }

        public void HidePointer()
        {
            _pointer.gameObject.SetActive(false);
        }

        public void ShowPointer()
        {
            _pointer.gameObject.SetActive(true);
        }




    }
}