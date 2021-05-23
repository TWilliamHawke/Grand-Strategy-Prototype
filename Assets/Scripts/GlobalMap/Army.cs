using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using GlobalMap;

[RequireComponent(typeof(NavMeshAgent))]
public class Army : MonoBehaviour, ISelectable, IHaveUnits
{
    public static event UnityAction<Army> OnArmySelected;
    public static event UnityAction OnArmyDeselected;

    [SerializeField] MeshRenderer _selector;
    [SerializeField] Animator _animator;

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

    private void CheckSpeed()
    {
        if(_navmeshAgent.remainingDistance > Mathf.Epsilon)
        {
            //Debug.Log("big speed");
            _animator.SetBool("IsWalk", true);
        }
        else
        {
            _animator.SetBool("IsWalk", false);
        }
    }

    private void MoveToClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if ((object)SelectionController.currentTarget != this) return;

            var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);

            if (Raycasts.SelectedTargetCanReachPoint(out var point))
            {
                _navmeshAgent.SetDestination(point);
            }
        }
    }

    public void MoveTo(Vector3 targetPoint)
    {
        _navmeshAgent.SetDestination(targetPoint);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
}
