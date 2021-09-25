using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using GlobalMap;
using GlobalMap.ArmyMovement;
using UnityEngine.EventSystems;

public class Army : MonoBehaviour, ISelectable, IHaveUnits
{
    public static event UnityAction<Army> OnArmySelected;
    public static event UnityAction<Army> OnArmyDeselected;
    public event UnityAction OnUnitAdd;

    [SerializeField] GlobalMapSelectable _selector;
    [SerializeField] MeshRenderer _selectionIndicator;
    [SerializeField] Animator _animator;

    IArmyMovementComponent _movementComponent;

    public List<Unit> unitList { get; set; } = new List<Unit>();

    public string localizedName => _armyName;
    string _armyName = "EmptyName";

    public void Deselect()
    {
        _selectionIndicator.gameObject.SetActive(false);
        OnArmyDeselected?.Invoke(this);
    }

    public void Select()
    {
        _selectionIndicator.gameObject.SetActive(true);
        OnArmySelected?.Invoke(this);
    }

    private void Awake()
    {
        _movementComponent = GetComponent<IArmyMovementComponent>();
    }

    void Update()
    {
        CheckDistance();
    }

    public void SetName(string settlementName)
    {
        _armyName = $"Army from {settlementName}";
    }

    public void SetTarget(Vector3 targetPoint)
    {
        _movementComponent.SetTarget(targetPoint);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public void AddUnit(UnitTemplate template)
    {
        var unit = new Unit(template, this);
        unitList.Add(unit);
        OnUnitAdd?.Invoke();
    }

    public void ForceStop()
    {
        _movementComponent.ForceStop();
        StopWalkAnimation();
    }

    public void Defeat(FactionData _)
    {
        _movementComponent.Retreat();
    }

    void StopWalkAnimation()
    {
        _animator.SetBool("IsWalk", false);
    }

    void CheckDistance()
    {
        if (_movementComponent.ShouldPlayWalkAnimation())
        {
            _animator.SetBool("IsWalk", true);
        }
        else
        {
            StopWalkAnimation();
        }
    }

}
