using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitStateManager : MonoBehaviour
{
    public Member member;
    public List<State<Member>> states;
    private Dictionary<Type, State<Member>> _statesByTypes;
    public State<Member> currentState;
    public State<Member> previousState;

    private void Awake()
    {
        member = GetComponent<Member>();
        _statesByTypes = new Dictionary<Type, State<Member>>();
        foreach (var state in states)
        {
            _statesByTypes.Add(state.GetType(),state);
            state.Init(member);
        }
        SetState(states[0].GetType());
    }

    public void SetState(Type var)
    {
        if (currentState)
        {
            currentState.Exit();
            previousState = currentState;
        }
        currentState = _statesByTypes[var];
        currentState.Enter();
    }

    public void BackToPrevious()
    {
        SetState(previousState.GetType());
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