using System;
using UnityEngine;

public class MemberDetection : MonoBehaviour
{
    public bool isDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            isDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isDetected = false;
    }
}