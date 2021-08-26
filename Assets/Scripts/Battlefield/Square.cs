using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Battlefield
{
    public class Square : MonoBehaviour
    {
        // static public event UnityAction<Square> OnPointerHide;
        
        [SerializeField] List<MeshRenderer> _frame;
        [SerializeField] BattleRules _rules;
        [SerializeField] Transform _pointer;
        [SerializeField] Transform _centerPosition;
        [SerializeField] Transform _pathArrow;

        [SerializeField] BattlefieldData _battlefieldData;

        // public bool EnemyOnSquare { get; set; } = false;

        //Directions _currentDirection = 0;
        // public Directions currentDirection => _currentDirection;


        // void Awake()
        // {
        //     SetDefaultFrameColor();
        //     _battlefieldData.AddNode(this);
        // }

        // public void UpdateFrameColors(Directions direction)
        // {
        //     UpdateFrameColors((int)direction);
        // }

        // public Vector2 GetPosition()
        // {
        //     float posX = transform.position.x / 10;
        //     float posZ = transform.position.z / 10;

        //     if (posX % 1 != 0 || posZ % 1 != 0)
        //     {
        //         Debug.LogError($"Wrong square position on {posX},{posZ}");
        //     }

        //     return new Vector2(posX, posZ);
        // }

        // public string StringifyPosition()
        // {
        //     return GetPosition().ToString();
        // }

        // public void UpdateFrameColors(int direction)
        // {
        //     if (_frame.Count != 8)
        //     {
        //         Debug.Log("Frame list isn't full!");
        //         return;
        //     }

        //     for (int i = 0; i < 8; i++)
        //     {
        //         int colorIndex = i - direction;
        //         if (colorIndex < 0)
        //         {
        //             colorIndex = 8 + i - direction;
        //         }

        //         _frame[i].materials[0].color = _rules.frameColors[colorIndex];
        //     }
        // }

        // public Directions RotatePointer(Vector3 position)
        // {
        //     _centerPosition.LookAt(position);
        //     int directionIndex = Mathf.RoundToInt(_centerPosition.eulerAngles.y / 45);
        //     var newDirection = (Directions)directionIndex;

        //     if (_currentDirection != newDirection)
        //     {
        //         float angleY = directionIndex * 45;
        //         //_pointer.transform.eulerAngles = new Vector3(0, angleY, 0);
        //         _pointer.transform.rotation = Quaternion.Euler(0, angleY, 0);
        //         UpdateFrameColors(directionIndex);
        //         _currentDirection = newDirection;
        //     }

        //     return newDirection;
        // }

        // public void SetDefaultFrameColor()
        // {
        //     SetFrameColor(_rules.defaultColor);
        // }

        // public void SetHoverColor()
        // {
        //     SetFrameColor(_rules.hoverColor);
        // }

        // public void HidePointer()
        // {
        //     _pointer.gameObject.SetActive(false);
        //     OnPointerHide?.Invoke(this);
        // }

        // public void ShowPointer()
        // {
        //     _pointer.gameObject.SetActive(true);
        // }

        // public void RotatePathArrow(Directions direction)
        // {
        //     _pathArrow.gameObject.SetActive(true);
        //     float angleY = (int)direction * 45;
        //     //_pathArrow.transform.eulerAngles = new Vector3(0, angleY, 0);
        //     _pathArrow.transform.rotation = Quaternion.Euler(0, angleY, 0);
        // }

        // public void RotatePathArrow(Square square)
        // {
        //     if(_battlefieldData.FindDirection(this, square, out var direction))
        //     {
        //         RotatePathArrow(direction);
        //     }
        // }

        // public void HidePathArrow()
        // {
        //     _pathArrow.gameObject.SetActive(false);
        // }

        // void SetFrameColor(Color color)
        // {
        //     foreach (var framePart in _frame)
        //     {
        //         framePart.materials[0].color = color;
        //     }
        // }


    }
}