using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO rewrite camera movement using a new input system
public class CameraController : MonoBehaviour
{
    public static Camera main;
    [SerializeField] UIScreensManager _screensManager;

    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _rotationX = 10f;
    [SerializeField] float _rotationY = 10f;
    [SerializeField] Camera _mainCamera;
    [SerializeField] float _scrollSpeed = 25f;
    [SerializeField] float _moveBorderThikness = 10f;
    [SerializeField] bool _enableMouseMovement = true;

    [Range(1, 5)]
    [SerializeField] int minY = 4;
    [SerializeField] int maxY = 20;

    void Awake() {
        main = _mainCamera;
    }

    void Update()
    {
        if(_screensManager.isAnyScreenOpen) return;
        
        transform.Translate(Movement());
        _mainCamera.transform.eulerAngles += Rotation(out var y);
        transform.Rotate(0, y, 0);
    }

    Vector3 Movement()
    {
        float x = 0;
        float z = 0;
        float y = Scroll();

        if (ShouldMoveRight())
        {
            x += _moveSpeed * Time.deltaTime;
        }
        else if (ShouldMoveLeft())
        {
            x -= _moveSpeed * Time.deltaTime;
        }

        if (ShouldMoveBack())
        {
            z -=_moveSpeed * Time.deltaTime;
        }
        else if (ShouldMoveForward())
        {
            z += _moveSpeed * Time.deltaTime;
        }

        return new Vector3(x, y, z);
    }


    float Scroll()
    {
        float deltaY = -Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
        float updatedY = transform.position.y + deltaY;
        
        if(updatedY < minY || updatedY > maxY) {
            return 0f;
        }
        else {
            return deltaY;
        }
    }

    Vector3 Rotation(out float y)
    {
        float x = 0;
        y = 0;

        if(Input.GetKey(KeyCode.F))
        {
            x += _rotationX * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.R))
        {
            x -= _rotationX * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            y -= _rotationY * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.E))
        {
            y += _rotationY * Time.deltaTime;
        }
        
        return new Vector3(x, 0, 0);

    }

    bool ShouldMoveForward()
    {
        return Input.GetKey(KeyCode.W)
               || (_enableMouseMovement && Input.mousePosition.y > Screen.height - _moveBorderThikness);
    }

    bool ShouldMoveBack()
    {
        return Input.GetKey(KeyCode.S)
               || (_enableMouseMovement && Input.mousePosition.y < _moveBorderThikness);
    }

    bool ShouldMoveLeft()
    {
        return Input.GetKey(KeyCode.A)
               || (_enableMouseMovement && Input.mousePosition.x < _moveBorderThikness);
    }

    bool ShouldMoveRight()
    {
        return Input.GetKey(KeyCode.D)
               || (_enableMouseMovement && Input.mousePosition.x > Screen.width - _moveBorderThikness);
    }
}
