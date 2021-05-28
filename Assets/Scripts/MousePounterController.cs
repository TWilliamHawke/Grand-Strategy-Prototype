using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePounterController : MonoBehaviour
{
    [SerializeField] Texture2D _defaultCursor;
    [SerializeField] Texture2D _moveCursor;
    [SerializeField] Texture2D _unwalkableCursor;

    [SerializeField] float offsetX = 8;
    [SerializeField] float offsetY = 10;

    CursorIconType _currentIcon = CursorIconType.noAction;
    CursorState _cursorState = CursorState.selection;

    void Awake()
    {
        SelectionController.OnSelectionCancel += GotoSelectionState;
        UnitListPanel.OnUnitCardSelection += GotoMovementState;
        Army.OnArmySelected += GotoMovementState;
    }

    void OnDestroy()
    {
        SelectionController.OnSelectionCancel -= GotoSelectionState;
        UnitListPanel.OnUnitCardSelection -= GotoMovementState;
        Army.OnArmySelected -= GotoMovementState;
    }

    void Start()
    {
        SetDefaultCursor();
    }

    void Update()
    {

        if (_cursorState == CursorState.selection) return;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            SetDefaultCursor();
            return;
        }

        if(TerrainIsWalkable())
        {
            SetMoveCursor();
        }
        else
        {
            SetUnwalkableCursor();
        }

    }

    private bool TerrainIsWalkable()
    {
        return Raycasts.SelectedTargetCanReachPoint(out var _);
    }

    void SetDefaultCursor()
    {
        if (_currentIcon == CursorIconType.standart) return;

        Cursor.SetCursor(_defaultCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        _currentIcon = CursorIconType.standart;
    }

    void SetMoveCursor()
    {
        if (_currentIcon == CursorIconType.move) return;

        Cursor.SetCursor(_moveCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        _currentIcon = CursorIconType.move;
    }

    void SetUnwalkableCursor()
    {
        if (_currentIcon == CursorIconType.noAction) return;

        Cursor.SetCursor(_unwalkableCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        _currentIcon = CursorIconType.noAction;
    }

    void GotoSelectionState()
    {
        SetDefaultCursor();
        _cursorState = CursorState.selection;
    }

    void GotoMovementState()
    {
        _cursorState = CursorState.movement;
    }

    void GotoMovementState(Army _)
    {
        GotoMovementState();
    }

    enum CursorIconType
    {
        standart,
        move,
        attack,
        noAction,
    }

    enum CursorState
    {
        selection,
        movement
    }
}
