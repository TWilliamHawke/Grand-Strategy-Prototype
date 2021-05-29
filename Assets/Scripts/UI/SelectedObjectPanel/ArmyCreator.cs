using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;

[RequireComponent(typeof(GarrisonView))]
public class ArmyCreator : MonoBehaviour
{
    [SerializeField] Army _armyPrefab;

    GarrisonView _garrisonView;

    void Awake() {
        _garrisonView = GetComponent<GarrisonView>();
    }

    void Update()
    {
        CreateArmyFormSelectedUnits();
    }

    void CreateArmyFormSelectedUnits()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (_garrisonView.AnyCardSelected())
        {
            if (Raycasts.SelectedTargetCanReachPoint(out var targetPoint))
            {
                SpawnArmy(targetPoint);
            }
        }
    }

    private void SpawnArmy(Vector3 targetPoint)
    {
        var newArmy = Instantiate(_armyPrefab, _garrisonView.settlementPosition, Quaternion.identity);
        newArmy.unitList = _garrisonView.GetSelectedUnits();
        _garrisonView.RemoveSelectedUnitsFromOwner();
        SelectionController.SetTarget(newArmy);
        newArmy.MoveTo(targetPoint);
    }
}
