using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionController : MonoBehaviour
{
    public static event System.Action<ISelectable> OnSelect;
    public static event System.Action OnSelectionCancel;

    static ISelectable _currentTarget;
    static public ISelectable currentTarget => _currentTarget;

    public static void SetTarget(ISelectable nextTarget)
    {
        _currentTarget?.Deselect();
        _currentTarget = nextTarget;
        nextTarget.Select();
        OnSelect?.Invoke(_currentTarget);
    }

    void Update()
    {
        SelectObjectByClick();
    }

    private void SelectObjectByClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        

        var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.TryGetComponent<ISelectable>(out var nextTarget))
            {
                _currentTarget?.Deselect();
                _currentTarget = nextTarget;
                nextTarget.Select();
                OnSelect?.Invoke(nextTarget);
            }
            else
            {
                if (_currentTarget == null) return;

                _currentTarget.Deselect();
                _currentTarget = null;
                OnSelectionCancel?.Invoke();
            }
        }
    }
}
