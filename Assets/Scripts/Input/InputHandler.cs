using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    private PlayerInputs _inputs;

    [Header("Camera")]
    public bool dragEnable;
    public bool turnEnable;

    public float horizontal;
    public float vertical;
    
    public UnityEvent toggleCrouchEvent;
    public UnityEvent<InputHandler> togglePauseEvent;
    private void Awake()
    {
        _inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        _inputs.Enable();

        #region Camera
        
        _inputs.Camera.Delta.performed += i =>
        {
            horizontal = i.ReadValue<Vector2>().x;
            vertical = i.ReadValue<Vector2>().y;
        };

        _inputs.Camera.Drag.performed += i => dragEnable = true;
        _inputs.Camera.Drag.canceled += i => dragEnable = false;

        _inputs.Camera.Turn.performed += i => turnEnable = true;
        _inputs.Camera.Turn.canceled += i => turnEnable = false;

        #endregion

        _inputs.Actions.Crouch.performed += i => toggleCrouchEvent.Invoke();
        _inputs.Actions.Pause.performed += i => togglePauseEvent.Invoke(this);
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
}