using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Raycasts
{
    public static bool SelectedTargetCanReachPoint(out Vector3 endPoint)
    {
        endPoint = new Vector3();
        var startPos = SelectionController.currentTarget?.transform;
        if (startPos == null) return false;

        var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo))
        {
            endPoint = hitInfo.point;

            if (NavMesh.Raycast(startPos.position, endPoint, out var hitinfo, NavMesh.AllAreas))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public static bool HitTarget<T>(out T target) where T : class
    {
        var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);
        target = null;

        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.TryGetComponent<T>(out var nextTarget))
            {
                target = nextTarget;
                return true;
            }
            return false;
        }
        return false;
    }

    public static bool HitTarget<T>(out T target, LayerMask layerMask) where T : class
    {
        var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);
        target = null;

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
        {
            if (hitInfo.collider.TryGetComponent<T>(out var nextTarget))
            {
                target = nextTarget;
                return true;
            }
            return false;
        }
        return false;
    }

    

    public static bool GetPosition(out Vector3 position)
    {
        position = Vector3.zero;
        var ray = CameraController.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            position = hitInfo.point;
            return true;
        }
        return false;
    }
}
