using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : MonoBehaviour
{
    public Unit unit;
    public List<State<Unit>> states;
    private Dictionary<Type, State<Unit>> _statesByTypes;
    public State<Unit> currentState;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        _statesByTypes = new Dictionary<Type, State<Unit>>();
        foreach (var state in states)
        {
            _statesByTypes.Add(state.GetType(),state);
            state.Init(unit);
        }
        SetState(states[0].GetType());
    }

    public void SetState(Type var)
    {
        if (currentState)
        {
            currentState.Exit();
        }
        currentState = _statesByTypes[var];
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
        currentState.ChangeState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
}