using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs _inputs;
    public InputVariables inputModel;

    private void Awake()
    {
        _inputs = new PlayerInputs();
        inputModel = GetComponent<InputVariables>();
    }

    private void OnEnable()
    {
        EnableStateInputs();
    }

    private void OnDisable()
    {
        DisableStateInputs();
        DisablePlay();
    }

    public void EnablePlay()
    {
        EnableCameraInputs();
        EnableMemberInputs();
    }
    
    public void DisablePlay()
    {
        DisableCameraInputs();
        DisableMemberInputs();
    }

    #region Camera Methods

    private void EnableCameraInputs()
    {
        _inputs.Camera.Delta.performed += GetDelta;
        _inputs.Camera.Drag.performed += Drag;
        _inputs.Camera.Drag.canceled += Drag;
        _inputs.Camera.Turn.performed += Turn;
        _inputs.Camera.Turn.canceled += Turn;
        _inputs.Camera.Zoom.performed += Zoom;
        _inputs.Camera.Zoom.canceled += Zoom;
        _inputs.Camera.Enable();
    }

    private void DisableCameraInputs()
    {
        _inputs.Camera.Delta.performed -= GetDelta;
        _inputs.Camera.Drag.performed -= Drag;
        _inputs.Camera.Drag.canceled -= Drag;
        _inputs.Camera.Turn.performed -= Turn;
        _inputs.Camera.Turn.canceled -= Turn;
        _inputs.Camera.Zoom.performed -= Zoom;
        _inputs.Camera.Zoom.canceled -= Zoom;
        _inputs.Camera.Disable();
    }

    private void GetDelta(InputAction.CallbackContext i) { inputModel.mouseDelta = i.ReadValue<Vector2>(); }
    private void Drag(InputAction.CallbackContext i) { inputModel.drag = !inputModel.drag; }
    private void Turn(InputAction.CallbackContext i) { inputModel.turn = !inputModel.turn; }
    private void Zoom(InputAction.CallbackContext i) { inputModel.zoomDelta = i.ReadValue<Vector2>(); }

    #endregion

    #region Member Methods

        private void EnableMemberInputs()
        {
            _inputs.MemberAction.Crouch.performed += HandleCrouch;
            _inputs.MemberAction.Enable();
        }
    
        private void DisableMemberInputs()
        {
            _inputs.MemberAction.Crouch.performed -= HandleCrouch;
            _inputs.MemberAction.Disable();
        }
        
        private void HandleCrouch(InputAction.CallbackContext i) { inputModel.onCrouchEvent.Invoke(); }

    #endregion

    #region State Methods

    private void EnableStateInputs()
    {
        _inputs.StateAction.Pause.performed += ExecutePause;
        _inputs.StateAction.Enable();
    }

    private void DisableStateInputs()
    {
        _inputs.StateAction.Pause.performed -= ExecutePause;
        _inputs.StateAction.Disable();
    }

    private void ExecutePause(InputAction.CallbackContext i)
    {
        Debug.Log("Pause");
    }

    #endregion
}