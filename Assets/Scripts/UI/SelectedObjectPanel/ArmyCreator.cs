using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;

[RequireComponent(typeof(GarrisonView))]
public class ArmyCreator : MonoBehaviour
{
    [SerializeField] Army _armyPrefab;

    GarrisonView _garrisonView;

    private void Awake() {
        _garrisonView = GetComponent<GarrisonView>();
    }

    private void Update()
    {
        SpawnArmyFormSelectedUnits();
    }

    private void SpawnArmyFormSelectedUnits()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (_garrisonView.AnyCardSelected())
        {
            if (Raycasts.SelectedTargetCanReachPoint(out var targetPoint))
            {
                var newArmy = Instantiate(_armyPrefab, _garrisonView.settlementPosition, Quaternion.identity);
                newArmy.unitList = _garrisonView.GetSelectedUnits();
                _garrisonView.RemoveSelectedUnitsFromOwner();
                SelectionController.SetTarget(newArmy);
                newArmy.MoveTo(targetPoint);
            }
        }
    }



}
