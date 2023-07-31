using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    #region Core Game Objects
    
    //Tüm oynanabilir bölümlerdeki elemanlar
    [Header("Core Game Object")]
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject managers;

    #endregion
    #region States
    
    [SerializeField] private PlayState playState;
    [SerializeField] private PauseState pauseState;
    [SerializeField] private DialogueState dialogueState;

    #endregion
    
    private Dictionary<Type, InGameBaseState> _statesByTypes = new Dictionary<Type, InGameBaseState>();
    private InGameBaseState _currentState;

    [Header("Manager References")]
    public CursorManager cursorManager;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void SetState(Type type)
    {
        _currentState?.Exit();

        _currentState = _statesByTypes[type];
        _currentState.Enter();
    }

    private void InitStates()
    {
        _statesByTypes = new Dictionary<Type, InGameBaseState>
        {
            { playState.GetType(), playState },
            { pauseState.GetType(), pauseState },
            { dialogueState.GetType(), dialogueState }
        };
        foreach (var keyValuePair in _statesByTypes)
        {
            keyValuePair.Value.Init(this);
        }
    }

    private void InitManagers()
    {
        cursorManager = GetComponentInChildren<CursorManager>();
    }

    private void InitCoreObjects()
    {
        parent.SetActive(true);
        canvas.SetActive(true);
        managers.SetActive(true);
    }

    private void SetInitState()
    {
        SetState(typeof(PlayState));
    }

    private void Init()
    {
        instance = this;
        
        InitStates();
        InitManagers();
        InitCoreObjects();
        SetInitState();
    }
}