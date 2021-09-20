using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.ArmyMovement
{
    public class ArmyMovementGraph : MonoBehaviour, IArmyMovementComponent
    {
        void Start()
        {

        }

        void Update()
        {

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
            return false;
        }

    }
}