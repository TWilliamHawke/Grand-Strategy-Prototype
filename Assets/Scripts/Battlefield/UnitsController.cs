using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{

    public class UnitsController : MonoBehaviour
    {
        [SerializeField] List<Animator> _unitList;
        [SerializeField] BoxCollider _units;

        [SerializeField] Transform _frontPosition;
        [SerializeField] Transform _centerPosition;
        [SerializeField] Transform _backPosition;

        public int numOfUnits => _unitList.Count;
        public bool unitsOnPosition => _units.transform.localPosition == _targetUnitsPosition;

        Vector3 _targetUnitsPosition;


        void Awake()
        {
            MoveUnitsToChunkCenter();
            _units.transform.localPosition = _targetUnitsPosition;
        }

        void Update()
        {
            if (!unitsOnPosition)
            {
                MoveToTarget();
            }
        }

        public void SetAnimatorValue(string name, bool value)
        {
            foreach (var unit in _unitList)
            {
                unit.SetBool(name, value);
            }
        }

        public int KillRandomUnit()
        {
            int randomIndex = Random.Range(0, _unitList.Count);
            _unitList[randomIndex].SetBool("IsDead", true);
            _unitList.RemoveAt(randomIndex);
            return _unitList.Count;
        }

        public void SetIsWalkValue(bool value)
        {
            SetAnimatorValue("IsWalk", value);
        }

        public void ChangeAnimationSpeed(float speedMult)
        {
            foreach (var unit in _unitList)
            {
                unit.speed = speedMult;
            }
        }

        public void MoveUnitsToChunkCenter()
        {
            _targetUnitsPosition = _centerPosition.localPosition;
        }

        public void MoveUnitsToChunkBack()
        {
            _targetUnitsPosition = _backPosition.localPosition;
        }

        private void MoveToTarget()
        {
            float speed = 1 * Time.deltaTime;
            var nextPosition = Vector3.MoveTowards(_units.transform.localPosition, _targetUnitsPosition, speed);
            _units.transform.localPosition = nextPosition;
        }

    }

}