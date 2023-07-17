using UnityEngine;

public class CameraController : MonoBehaviour
{
    public InputHandler inputHandler;
    public float camDragSpeed;
    public float camTurnSpeed;

    private void Update() 
    {
        DragCamera();
        TurnCamera();
    }

    private void DragCamera()
    {
        if (inputHandler.dragEnable)
        {
            transform.position -= transform.forward * (inputHandler.vertical * (camDragSpeed * Time.deltaTime));
            transform.position -= transform.right * (inputHandler.horizontal * (camDragSpeed * Time.deltaTime));
        }
    }

    private void TurnCamera()
    {
        if (inputHandler.turnEnable)
        {
            transform.Rotate(0,inputHandler.horizontal * Time.deltaTime * camTurnSpeed,0);
        }
    }
}
