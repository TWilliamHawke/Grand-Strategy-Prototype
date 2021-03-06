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
    [SerializeField] int _minHeight = 4;
    [SerializeField] int _maxHeight = 20;

    void Awake()
    {
        main = _mainCamera;
    }

    void Update()
    {
        if (_screensManager.isAnyScreenOpen) return;

        MoveCamera();
        RotateCameraX();
        RotateCameraY();
    }

    void MoveCamera()
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
            z -= _moveSpeed * Time.deltaTime;
        }
        else if (ShouldMoveForward())
        {
            z += _moveSpeed * Time.deltaTime;
        }

        transform.Translate(new Vector3(x, y, z));

    }

    float Scroll()
    {
        float deltaY = -Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
        float updatedY = transform.position.y + deltaY;

        if (updatedY < _minHeight || updatedY > _maxHeight)
        {
            return 0f;
        }
        else
        {
            return deltaY;
        }
    }

    void RotateCameraX()
    {
        float x = 0;

        if (Input.GetKey(KeyCode.F))
        {
            x += _rotationX * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.R))
        {
            x -= _rotationX * Time.deltaTime;
        }

        _mainCamera.transform.Rotate(Vector3.right, x);
    }

    void RotateCameraY()
    {
        float y = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            y -= _rotationY * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            y += _rotationY * Time.deltaTime;
        }
        
        transform.Rotate(Vector3.up, y);
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
