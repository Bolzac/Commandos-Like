using System;
using UnityEngine;

[Serializable]
public class CursorToWorld
{
    public Camera cam;
    public LayerMask groundLayer;
    
    public Vector3 GetMouseWorldPosition(Vector3 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}