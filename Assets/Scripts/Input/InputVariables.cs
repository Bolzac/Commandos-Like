using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input Variables")]
public class InputVariables : ScriptableObject
{
    public Vector2 mouseDelta;
    public Vector2 zoomDelta;

    public bool drag;
    public bool turn;

    public bool isPaused;

    public void ResetVariables()
    {
        mouseDelta = Vector2.zero;
        zoomDelta = Vector2.zero;
        drag = false;
        turn = false;
        isPaused = false;
    }
}