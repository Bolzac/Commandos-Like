using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    #region Core Game Objects
    
    //Tüm oynanabilir bölümlerdeki elemanlar
    [Header("Core Game Object")]
    [SerializeField] private GameObject canvas;

    #endregion
    #region States
    
    [SerializeField] private PlayState playState;
    [SerializeField] private PauseState pauseState;
    [SerializeField] private DialogueState dialogueState;

    #endregion

    #region Managers
    
    public InputManager inputManager;
    public UIManager uiManager;
    public SelectionManager selectionManager;

    #endregion

    public Animator animator;
    
    private Dictionary<Type, InGameBaseState> _statesByTypes = new Dictionary<Type, InGameBaseState>();
    private InGameBaseState _currentState;
    private InGameBaseState _previousState;

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
        _previousState = _currentState;
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
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        inputManager = transform.GetComponentInChildren<InputManager>();
        uiManager = transform.GetComponentInChildren<UIManager>();
        selectionManager = transform.GetComponentInChildren<SelectionManager>();
    }
    
    private void InitCoreObjects()
    {
        canvas.SetActive(true);
    }

    private void SetInitState()
    {
        SetState(typeof(PlayState));
    }

    private void Init()
    {
        InitStates();
        InitManagers();
        InitCoreObjects();
        SetInitState();
    }
}