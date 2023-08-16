using System;
using Cinemachine;
using UnityEngine;

[Serializable]
public class ZoomCamera
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    
    public void Zoom(InputVariables inputModel, CinemachineVirtualCamera virtualCamera)
    {
        if(inputModel.zoomDelta.y == 0) return;

        virtualCamera.m_Lens.OrthographicSize =
            Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize + inputModel.zoomDelta.y * zoomSpeed * Time.deltaTime, minZoom,
                maxZoom);
    }
}