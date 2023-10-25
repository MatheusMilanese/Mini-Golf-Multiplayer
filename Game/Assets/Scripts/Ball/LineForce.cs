using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class LineForce : MonoBehaviour
{
    private BallController ballController;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Layers")]
    [SerializeField] private LayerMask layerBall;
    [SerializeField] private LayerMask layerWall;
    
    [Header("Settings")]
    [SerializeField] private float shootForce;
    [SerializeField] private float maxDistance;

    private bool isAiming;
    Vector3? worldPoint;

    private void Awake() {
        ballController = GetComponent<BallController>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        OnAiming();
    }

    private void OnMouseDown() {
        if(ballController.isIdle && MouseInfo.CastMouseClickRay(layerBall).HasValue){
            isAiming = true;
        }
    }

    void OnAiming(){
        if(!isAiming || !ballController.isIdle) return;
        
        worldPoint = MouseInfo.CastMouseClickRay(layerWall);
        
        if(!worldPoint.HasValue) return;
        
        DrawLineForce(worldPoint.Value);

        if(Input.GetMouseButtonUp(0)){
            isAiming = false;
            lineRenderer.enabled = false;
            OnShoot(worldPoint.Value);
        }
    }

    void OnShoot(Vector3 worldMousePosition){
        Vector3 direction = transform.position - worldMousePosition;
        float distance = Math.Min(Vector3.Distance(transform.position, worldMousePosition), maxDistance);
        ballController.MoveBall(direction, distance*shootForce);
    }

    void DrawLineForce(Vector3 worldPosition){
        Vector3 direction = (transform.position - worldPosition).normalized;
        float distance = Math.Min(Vector3.Distance(transform.position, worldPosition), maxDistance);
        Vector3[] positions = { 
            transform.position,
            transform.position + new Vector3(direction.x, 0, direction.z)*distance
        };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }
}
