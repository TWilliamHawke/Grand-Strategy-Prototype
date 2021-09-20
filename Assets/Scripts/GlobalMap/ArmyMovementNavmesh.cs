using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace GlobalMap.ArmyMovement
{
    public interface IArmyMovementComponent
    {
        void ForceStop();
        void Retreat();
        void SetTarget(Vector3 targetPoint);
        bool ShouldPlayWalkAnimation();
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class ArmyMovementNavmesh : MonoBehaviour, IArmyMovementComponent
    {
        [SerializeField] NavMeshAgent _navmeshAgent;
        [SerializeField] GlobalMapSelectable _selector;

        ISelectable _armyComponent;

        private void Awake()
        {
            _armyComponent = GetComponent<Army>() as ISelectable;
        }

        void Update()
        {
            MoveToClick();
        }

        public void SetTarget(Vector3 targetPoint)
        {
            _navmeshAgent.SetDestination(targetPoint);
            _navmeshAgent.isStopped = false;
        }

        public void ForceStop()
        {
            _navmeshAgent.isStopped = true;
        }

        public void Retreat()
        {
            var targetPos = transform.position - transform.forward * 2;
            SetTarget(targetPos);
        }

        public bool ShouldPlayWalkAnimation()
        {
            return _navmeshAgent.remainingDistance > Mathf.Epsilon;
        }

        //TODO move this into special controller
        void MoveToClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_selector.selectedObject != _armyComponent) return;
                if (EventSystem.current.IsPointerOverGameObject()) return;

                var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);

                if (Raycasts.SelectedTargetCanReachPoint(_selector.selectedObject, out var point))
                {
                    SetTarget(point);
                }
            }
        }


    }
}