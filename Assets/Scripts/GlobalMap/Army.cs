using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using GlobalMap;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class Army : MonoBehaviour, ISelectable, IHaveUnits
{
    public static event UnityAction<Army> OnArmySelected;
    public static event UnityAction OnArmyDeselected;
    public event UnityAction OnUnitAdd;

    [SerializeField] MeshRenderer _selector;
    [SerializeField] Animator _animator;
    [SerializeField] UnitsListController _unitListController;

    NavMeshAgent _navmeshAgent;

    public List<Unit> unitList { get; set; } = new List<Unit>();

    public Vector3 position => transform.position;

    public void Deselect()
    {
        _selector.gameObject.SetActive(false);
        OnArmyDeselected?.Invoke();
    }

    public void Select()
    {
        _selector.gameObject.SetActive(true);
        OnArmySelected?.Invoke(this);
    }

    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveToClick();
        CheckSpeed();

    }

    public void MoveTo(Vector3 targetPoint)
    {
        _navmeshAgent.SetDestination(targetPoint);
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
        _navmeshAgent.isStopped = true;
        _animator.SetBool("IsWalk", false);
    }

    public void Retreat()
    {
        var targetPos = transform.position - transform.forward * 2;
        _navmeshAgent.SetDestination(targetPos);
    }

    public void Defeat()
    {
        Deselect();
        Destroy(this);
    }

    void CheckSpeed()
    {
        if (_navmeshAgent.remainingDistance <= Mathf.Epsilon || _navmeshAgent.isStopped)
        {
            _animator.SetBool("IsWalk", false);
        }
        else
        {
            _animator.SetBool("IsWalk", true);
        }
    }

    void MoveToClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if ((object)SelectionController.currentTarget != this) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);

            if (Raycasts.SelectedTargetCanReachPoint(out var point))
            {
                _navmeshAgent.isStopped = false;
                _navmeshAgent.SetDestination(point);
            }
        }
    }

}
