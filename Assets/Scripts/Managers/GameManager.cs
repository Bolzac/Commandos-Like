using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    [Header("SubHandlers")] 
    public SceneHandler sceneHandler;

    #region Game State
    
    public InGameState inGameState;
    public LevelSelectionState levelSelectionState;
    public MainMenuState mainMenuState;

    private Dictionary<Type, GameState> _statesByType;
    private GameState _currentState;
    
    #endregion

    #region Managers

    public InGameManager inGameManager;

    #endregion

    private void Awake()
    {
        _instance = this;
        
        InitManagers();
        InitStates();
        
        SetState(typeof(InGameState));
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneHandler = new SceneHandler();
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void SetState(Type type)
    {
        _currentState?.Exit();

        _currentState = _statesByType[type];
        _currentState.Enter();
    }

    private void InitStates()
    {
        _statesByType = new Dictionary<Type, GameState>
        {
            { mainMenuState.GetType(), mainMenuState },
            { inGameState.GetType(), inGameState },
            { levelSelectionState.GetType(), levelSelectionState }
        };
        foreach (var keyValuePair in _statesByType)
        {
            keyValuePair.Value.Init(this);
        }
    }

    private void InitManagers()
    {
        inGameManager = GetComponentInChildren<InGameManager>(true);
    }
}