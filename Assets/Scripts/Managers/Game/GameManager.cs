using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("SubManagers")] 
    public LevelManager levelManager;
    public SoundManager soundManager;

    #region Game State
    
    public InGameState inGameState;
    public MainMenuState mainMenuState;

    private Dictionary<Type, GameState> _statesByType;
    private GameState _currentState;
    
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);

        levelManager = GetComponentInChildren<LevelManager>();
        soundManager = GetComponentInChildren<SoundManager>();

        InitStates();
        
        SetState(typeof(MainMenuState));
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
            { inGameState.GetType(), inGameState }
        };
        foreach (var keyValuePair in _statesByType)
        {
            keyValuePair.Value.Init(this);
        }
    }
}