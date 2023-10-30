using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseInfo {
      
    public static Vector3? CastMouseClickRay(LayerMask layer){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePositionFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePositionNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        if(Physics.Raycast(worldMousePositionNear, worldMousePositionFar-worldMousePositionNear, out hit, float.PositiveInfinity, layer)){
            return hit.point;
        }
        else{
            return null;
        }
    }
}
