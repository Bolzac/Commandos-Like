using UnityEngine;
public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    
    [SerializeField] private Texture2D defaultCursor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ReturnDefaultCursor();
    }

    public void ReturnDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor,Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetCursor(Texture2D texture2D)
    {
        Cursor.SetCursor(texture2D,Vector2.zero, CursorMode.ForceSoftware);
    }
}