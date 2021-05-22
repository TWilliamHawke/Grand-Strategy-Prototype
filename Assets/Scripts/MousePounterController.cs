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

    CursorIconType currentIcon = CursorIconType.noAction;
    CursorState cursorState = CursorState.selection;

    void Awake()
    {
        SelectionController.OnSelectionCancel += GotoSelectionState;
        UnitsView.OnUnitCardSelection += GotoMovementState;
        Army.OnArmySelected += GotoMovementState;
    }

    private void OnDestroy()
    {
        SelectionController.OnSelectionCancel -= GotoSelectionState;
        UnitsView.OnUnitCardSelection -= GotoMovementState;
        Army.OnArmySelected -= GotoMovementState;
    }

    void Start()
    {
        SetDefaultCursor();
    }

    void Update()
    {

        if (cursorState == CursorState.selection) return;
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
        if (currentIcon == CursorIconType.standart) return;

        Cursor.SetCursor(_defaultCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        currentIcon = CursorIconType.standart;
    }

    void SetMoveCursor()
    {
        if (currentIcon == CursorIconType.move) return;

        Cursor.SetCursor(_moveCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        currentIcon = CursorIconType.move;
    }

    void SetUnwalkableCursor()
    {
        if (currentIcon == CursorIconType.noAction) return;

        Cursor.SetCursor(_unwalkableCursor, new Vector2(offsetX, offsetY), CursorMode.Auto);
        currentIcon = CursorIconType.noAction;
    }

    void GotoSelectionState()
    {
        SetDefaultCursor();
        cursorState = CursorState.selection;
    }

    void GotoMovementState()
    {
        cursorState = CursorState.movement;
    }

    void GotoMovementState(Army army)
    {
        GotoMovementState();
    }

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