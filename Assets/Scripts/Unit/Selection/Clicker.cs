using System;
using UnityEngine;
[Serializable]
public class Clicker
{
    public float clickSpacing;
    private int _clickTime;
    private float _counter;
    
    public void ClickCounter(UnitManager unitManager)
    {
        if (Input.GetMouseButtonUp(0))
        {
            unitManager.teamController.DisableRunning();
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
                unitManager.teamController.HandleRunning();
                _counter = 0;
                _clickTime = 0;
            }
        }
    }
}