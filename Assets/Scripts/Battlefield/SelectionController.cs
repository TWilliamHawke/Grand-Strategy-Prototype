using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
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
        HoverChunk();
        SelectUnit();
        SelectTargetChunk();
    }

    void HoverChunk()
    {
        if (Input.GetMouseButton(1)) return;

        if (Raycasts.HitTarget<Chunk>(out var chunk, _gridLayer))
        {
            _selectedObjects.SetHoveredChunk(chunk);
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
    void SelectTargetChunk()
    {
        if (!Input.GetMouseButtonUp(1)) return;
        if (!_selectedObjects.troop) return;

        if (Raycasts.HitTarget<Chunk>(out var chunk, _gridLayer))
        {
            var node = _battlefieldData.FindNode(chunk);
            _selectedObjects.troop.CreatePathTo(node);
        }
    }
}

}

