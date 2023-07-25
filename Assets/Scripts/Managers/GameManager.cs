using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is Null!");
            }
            return _instance;
        }
    }

    [Header("Variables")]
    public bool isGamePaused;

    [Header("SubHandlers")] 
    public SceneHandler sceneHandler;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }

    private void Start()
    {
        sceneHandler = new SceneHandler();
    }

    public void TogglePauseTheGame(InputHandler inputHandler)
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            inputHandler.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            inputHandler.enabled = true;
            Time.timeScale = 1;
        }
    }
}