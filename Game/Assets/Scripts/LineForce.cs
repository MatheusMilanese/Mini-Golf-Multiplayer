using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        Vector3? worldPoint = CastMouseClickRay();

        if(!worldPoint.HasValue){
            return;
        }

        DrawLineForce(worldPoint.Value);
    }

    void DrawLineForce(Vector3 worldPosition){
        Vector3 distance = transform.position - worldPosition;
        Vector3[] positions = { transform.position, transform.position+new Vector3(distance.x, 0, distance.z)};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    Vector3? CastMouseClickRay(){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePositionFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePositionNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        if(Physics.Raycast(worldMousePositionNear, worldMousePositionFar-worldMousePositionNear, out hit, float.PositiveInfinity)){
            return hit.point;
        }
        else{
            return null;
        }
    }
}
