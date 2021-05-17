using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{

public class SelectionController : MonoBehaviour
{
    [SerializeField] SelectedObjects _selectedObjects;
    [SerializeField] BattlefieldData _battlefieldData;

    [SerializeField] LayerMask _unitsLayer;
    [SerializeField] LayerMask _gridLayer;

    void Update()
    {
        HoverSquare();
        SelectUnit();
        SelectTargetSquare();
    }

    void HoverSquare()
    {
        if (Input.GetMouseButton(1)) return;

        if (Raycasts.HitTarget<Square>(out var square, _gridLayer))
        {
            _selectedObjects.SetHoveredSquare(square);
        }
    }

    void SelectUnit()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (Raycasts.HitTarget<BoxCollider>(out var units, _unitsLayer))
        {
            if(units.transform.parent.gameObject.TryGetComponent<Troop>(out var troop))
            {
                if(troop.ownedByPlayer)
                {
                    _selectedObjects.SetSelectedTroop(troop);
                }
            }
        }
    }

    //unit movement target
    void SelectTargetSquare()
    {
        if (!Input.GetMouseButtonUp(1)) return;
        if (!_selectedObjects.troop) return;

        if (Raycasts.HitTarget<Square>(out var square, _gridLayer))
        {
            _selectedObjects.troop.CreatePathTo(square);
        }
    }
}

}

