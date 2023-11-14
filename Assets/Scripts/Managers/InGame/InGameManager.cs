using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    
    #region Core Game Objects
    
    //Tüm oynanabilir bölümlerdeki elemanlar
    [Header("Core Game Object")]
    [SerializeField] private GameObject canvas;

    #endregion
    #region States
    
    [SerializeField] private PlayState playState;
    [SerializeField] private PauseState pauseState;
    //[SerializeField] private DialogueState dialogueState;

    public DialogueState dialogueState;
    #endregion
    #region Managers
    
    public InputManager inputManager;
    public UIManager uiManager;
    public SelectionManager selectionManager;
    public CameraController cameraController;
    public DialogueManager dialogueManager;

    #endregion

    public Animator animator;
    
    private Dictionary<Type, InGameBaseState> _statesByTypes = new Dictionary<Type, InGameBaseState>();
    public InGameBaseState currentState;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SetState(Type type)
    {
        currentState?.Exit();
        currentState = _statesByTypes[type];
        currentState.Enter();
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
        dialogueManager = transform.GetComponentInChildren<DialogueManager>();
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