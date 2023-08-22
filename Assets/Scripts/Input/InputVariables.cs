using UnityEngine;
using UnityEngine.Events;

public class InputVariables : MonoBehaviour
{
    public Vector2 mouseDelta;
    public Vector2 zoomDelta;

    public bool drag;
    public bool turn;

    public bool isPaused;

    public UnityEvent onCrouchEvent;
    public UnityEvent onDoubleClick;
    public UnityEvent onOneClick;
}