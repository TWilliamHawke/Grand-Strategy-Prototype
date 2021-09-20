using System.Collections;
using System.Collections.Generic;
using GlobalMap;
using UnityEngine;

[RequireComponent(typeof(GarrisonView))]
public class ArmyCreator : MonoBehaviour
{
    [SerializeField] Army _armyPrefabNavmesh;
    [SerializeField] Army _armyPrefabGraph;
    [SerializeField] GlobalMapSelectable _selector;
    [SerializeField] bool _useNavmesh;

    GarrisonView _garrisonView;

    void Awake()
    {
        _garrisonView = GetComponent<GarrisonView>();
    }

    void Update()
    {
        CreateArmyByRightClick();
    }

    //click handler in editor
    public void CreateArmyByButton()
    {
        if (!_garrisonView.AnyCardSelected()) return;
        SpawnArmy(_armyPrefabGraph);
    }

    void CreateArmyByRightClick()
    {
        if (!_useNavmesh || !Input.GetMouseButtonDown(1)) return;
        if (!_garrisonView.AnyCardSelected()) return;

        if (Raycasts.SelectedTargetCanReachPoint(_selector.selectedObject, out var targetPoint))
        {
            var newArmy = SpawnArmy(_armyPrefabNavmesh);
            newArmy.SetTarget(targetPoint);
        }
    }

    Army SpawnArmy(Army armyPrefab)
    {
        var newArmy = Instantiate(armyPrefab, _garrisonView.settlementPosition, Quaternion.identity);
        newArmy.unitList = _garrisonView.GetSelectedUnits();
        newArmy.SetName(_garrisonView.settlementName);

        _garrisonView.RemoveSelectedUnitsFromOwner();
        _selector.Select(newArmy);
        return newArmy;
    }
}
