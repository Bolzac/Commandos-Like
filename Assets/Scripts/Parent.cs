using UnityEngine;

public class Parent : MonoBehaviour
{
    public CameraController cameraController;
    public TeamManagement teamManagement;

    private void Awake()
    {
        cameraController = GetComponentInChildren<CameraController>(true);
        teamManagement = GetComponentInChildren<TeamManagement>(true);
        
        cameraController.gameObject.SetActive(true);
        teamManagement.gameObject.SetActive(true);
        
        teamManagement.Init(this);
        cameraController.Init(this);
    }
}