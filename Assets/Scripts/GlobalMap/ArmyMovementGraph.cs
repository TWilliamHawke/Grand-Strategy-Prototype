using System.Collections;
using System.Collections.Generic;
using Battlefield;
using GlobalMap.Regions;
using PathFinding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GlobalMap.ArmyMovement
{
    public class ArmyMovementGraph : MonoBehaviour, IArmyMovementComponent
    {
        [SerializeField] LayerMask _mapMeshLayer;
        [Header("Prefab parts")]
        [SerializeField] Army _army;
        [SerializeField] TroopStateIndicator _indicator;
        [Header("Scriptable objects")]
        [SerializeField] RegionsList _regionsList;
        [SerializeField] Timer _timer;
        [SerializeField] GlobalMapSelectable _selector;

        ISelectable _armyComponent;

        Stack<RegionNode> _path = new Stack<RegionNode>();

        int _movementProgress = 0;

        Region _currentRegion;

        //HACK _currentRegion should be set by public method
        //in army creator
        void Awake()
        {
            _currentRegion = (_selector.selectedObject as RegionMesh)?.region;
            _armyComponent = _army as ISelectable;
            _timer.OnTick += ChangeProgress;
            _indicator.UpdateProgress(0);
            Army.OnArmySelected += SelectHandler;
            Army.OnArmyDeselected += DeselectHandler;
        }

        void OnDestroy()
        {
            _timer.OnTick -= ChangeProgress;
            Army.OnArmySelected -= SelectHandler;
            Army.OnArmyDeselected -= DeselectHandler;
        }

        void Update()
        {
            MoveToClick();
        }

        public void ForceStop()
        {
            Debug.Log("force stop");

        }

        public void Retreat()
        {
            Debug.Log("retreat");
        }

        public void SetTarget(Vector3 targetPoint)
        {
            Debug.Log(targetPoint);
        }

        public bool ShouldPlayWalkAnimation()
        {
            return _path.Count > 0;
        }

        //TODO move this into special controller
        void MoveToClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_selector.selectedObject != _armyComponent) return;
                if (EventSystem.current.IsPointerOverGameObject()) return;

                if (Raycasts.HitTarget<RegionMesh>(out var mesh, _mapMeshLayer))
                {
                    SetTarget(mesh.region);
                }
            }
        }

        void SetTarget(Region region)
        {
            if (_currentRegion == region)
            {
                //stopMovement
                HidePath();
                _path.Clear();
                _movementProgress = 0;
                UpdateVisualProgress();
            }
            else
            {
                var pathFinder = new PathFinder<RegionNode>(_currentRegion.node, region.node, _regionsList);
                _path = pathFinder.GetPath();
                ShowPath();
            }
            UpdateRotation();

        }

        private void UpdateRotation()
        {
            transform.position = _currentRegion.node.nodeCenter;
            if(_path.Count == 0)
            {
                transform.rotation = Quaternion.identity;
                return;
            }
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, _path.Peek().nodeCenter - _currentRegion.node.nodeCenter);
            transform.Translate(Vector3.forward * 2);
        }

        void ShowPath()
        {
            var region = _currentRegion;

            foreach (var node in _path)
            {
                region.RotatePathArrow(node);
                region = node.region;
            }
        }

        void HidePath()
        {
            var region = _currentRegion;
            foreach (var node in _path)
            {
                region.HidePathArrow();
                region = node.region;
            }

        }

        void ChangeProgress()
        {
            if (_path.Count == 0) return;

            _movementProgress++;


            if (_movementProgress == 10)
            {
                _movementProgress = 0;
                _currentRegion.HidePathArrow();
                var nextNode = _path.Pop();
                _currentRegion = nextNode.region;
                transform.position = nextNode.nodeCenter;
                UpdateRotation();
            }
            UpdateVisualProgress();
        }

        void UpdateVisualProgress()
        {
            _indicator.UpdateProgress(_movementProgress / 10f);
        }

        void SelectHandler(Army army)
        {
            if (army != _army) return;

            _indicator.SetSelectedBackground();
            ShowPath();
        }
        void DeselectHandler(Army army)
        {
            if (army != _army) return;

            _indicator.SetDefaultBackground();
            HidePath();
        }
    }
}