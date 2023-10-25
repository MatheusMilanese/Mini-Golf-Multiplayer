using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private LayerMask layer;

    private void Update() {
        if(MouseInfo.CastMouseClickRay(layer).HasValue) return;

        if(Input.GetMouseButtonDown(0)){
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
        }
        if(Input.GetMouseButtonUp(0)){
            freeLookCamera.m_YAxis.m_InputAxisName = "";
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
        }
    }
}
