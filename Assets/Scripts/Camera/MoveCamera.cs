using System;
using UnityEngine;

[Serializable]
public class MoveCamera
{
    [SerializeField] private float moveSpeed;
    private Transform _temp;
    private Vector3 _position;
    private Vector3 _rotation;

    public void MoveCam(InputVariables inputModel, Transform follow)
    {
        if(!inputModel.drag) return;

        _temp = follow;
        _rotation = _temp.rotation.eulerAngles;
        _rotation.x = 0;
        _temp.rotation = Quaternion.Euler(_rotation);
        _position = _temp.position;
        _position -= _temp.forward * (inputModel.mouseDelta.y * (moveSpeed * Time.deltaTime));
        _position -= _temp.right * (inputModel.mouseDelta.x * (moveSpeed * Time.deltaTime));
        _rotation.x = 45;
        _temp.rotation = Quaternion.Euler(_rotation);
        follow.position = _position;
    }
}