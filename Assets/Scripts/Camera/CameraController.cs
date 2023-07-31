using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : Child
{
    [SerializeField] private InputVariables inputModel;
    
    private Transform _transform;
    private Vector3 position;
    private Vector3 _temp;

    [SerializeField] private Transform cam;
    [SerializeField] private float camDragSpeed;
    [SerializeField] private float camTurnSpeed;
    [SerializeField] private float zoomSpeed;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private void Awake()
    {
        _transform = transform;
    }

    public void FocusOnCharacter()
    {
        transform.position = runner.teamManagement.mainMember.transform.position;
    }

    public void DragCamera()
    {
        if(!inputModel.drag) return; 
        position = _transform.position;
        position -= _transform.forward * (inputModel.mouseDelta.y * (camDragSpeed * Time.deltaTime));
        position -= _transform.right * (inputModel.mouseDelta.x * (camDragSpeed * Time.deltaTime));
        transform.position = position;
    }

    public void TurnCamera()
    {
        if(!inputModel.turn) return;
        transform.Rotate(0,inputModel.mouseDelta.x * Time.deltaTime * camTurnSpeed,0);
    }

    public void ZoomCamera()
    {
        if(inputModel.zoomDelta.y == 0) return;
        
        _temp = cam.localPosition;
        _temp.z += inputModel.zoomDelta.y * zoomSpeed * Time.deltaTime;

        _temp.z = Mathf.Clamp(_temp.z, minZoom, maxZoom);
        
        cam.localPosition = _temp;
    }
}