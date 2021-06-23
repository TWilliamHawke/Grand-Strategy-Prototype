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
    [SerializeField] Texture2D _attackCursor;

    [SerializeField] float offsetX = 8;
    [SerializeField] float offsetY = 10;

    [SerializeField] GlobalMapSelectable _selector;

    CursorIconType _currentIcon = CursorIconType.noAction;
    CursorState _cursorState = CursorState.selection;

    void Awake()
    {
        _selector.OnSelectionCancel += GotoSelectionState;
        UnitListPanel.OnUnitCardSelection += GotoMovementState;
        Army.OnArmySelected += GotoMovementState;
    }

    void OnDestroy()
    {
        _selector.OnSelectionCancel -= GotoSelectionState;
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

        if(CursorHoverEnemy())
        {
            SetAttackCursor();
            return;
        }
        
        if (TerrainIsWalkable())
        {
            SetMoveCursor();
        }
        else
        {
            SetUnwalkableCursor();
        }

    }

    bool TerrainIsWalkable()
    {
        return Raycasts.SelectedTargetCanReachPoint(_selector.selectedObject, out var _);
    }

    bool CursorHoverEnemy()
    {
        if(Raycasts.HitTarget<Settlement>(out var target))
        {
            return !target.isPlayerSettlement;
        }
        return false;
    }

    void SetDefaultCursor()
    {
        ChangeCursor(CursorIconType.standart, _defaultCursor);
    }

    void SetAttackCursor()
    {
        ChangeCursor(CursorIconType.attack, _attackCursor);
    }

    void SetMoveCursor()
    {
        ChangeCursor(CursorIconType.move, _moveCursor);
    }

    void SetUnwalkableCursor()
    {
        ChangeCursor(CursorIconType.noAction, _unwalkableCursor);
    }

    void ChangeCursor(CursorIconType type, Texture2D icon)
    {
        if (_currentIcon == type) return;

        Cursor.SetCursor(icon, new Vector2(offsetX, offsetY), CursorMode.Auto);
        _currentIcon = type;
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
