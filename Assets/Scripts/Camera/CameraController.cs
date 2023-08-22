using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    #region NewSystem

    [Header("New System")] 
    public InputVariables inputModel;
    
    [Header("Camera Properties")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("Camera Methods")] 
    [SerializeField] private ZoomCamera zoomCamera;
    [SerializeField] private MoveCamera moveCamera;
    [SerializeField] private TurnCamera turnCamera;

    [Header("Camera Duration")] 
    [SerializeField] private float duration;
    private float elapsedTime;
    private Coroutine _coroutine;

    public void HandleCameraLocomotion()
    {
        moveCamera.MoveCam(inputModel,transform);
        zoomCamera.Zoom(inputModel,virtualCamera);
        turnCamera.TurnCam(inputModel,transform);
    }

    public void StartFocusCoroutine(int index)
    {
       if(_coroutine == null) StartCoroutine(FocusOnMember(index));
       else StopCoroutine(_coroutine);
    }

    private IEnumerator FocusOnMember(int index)
    {
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position,TeamManagement.Instance.members[index].transform.position,(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = TeamManagement.Instance.members[index].transform.position;
        yield return null;
    }

    #endregion
}