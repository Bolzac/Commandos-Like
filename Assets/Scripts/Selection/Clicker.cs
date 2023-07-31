using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Clicker
{
    public UnityEvent onOneClick;
    public UnityEvent onDoubleClick;
    
    public float clickSpacing;
    private int _clickTime;
    private float _counter;
    
    public void ClickCounter()
    {
        if (Input.GetMouseButtonUp(0))
        {
            onOneClick.Invoke(); //TeamController.DisableRunning
            _clickTime++;
        }
        
        if (_clickTime > 0)
        {
            _counter += Time.deltaTime;
            if (_counter >= clickSpacing)
            {
                _counter = 0;
                _clickTime = 0;
            }
            else if(_clickTime == 2)
            {
                onDoubleClick.Invoke(); //TeamController.HandleRunning
                _counter = 0;
                _clickTime = 0;
            }
        }
    }
}