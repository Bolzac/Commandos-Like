using System;
using UnityEngine;

[Serializable]
public class TurnCamera
{
    [SerializeField] private float camTurnSpeed;

    public void TurnCam(InputVariables inputModel, Transform follow)
    {
        if (!inputModel.turn) return;
        follow.Rotate(Vector3.up,inputModel.mouseDelta.x * Time.deltaTime * camTurnSpeed,Space.World);
    }
}