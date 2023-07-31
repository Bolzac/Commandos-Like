using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitStateManager : MonoBehaviour
{
    [FormerlySerializedAs("unit")] public Member member;
    public List<State<Member>> states;
    private Dictionary<Type, State<Member>> _statesByTypes;
    public State<Member> currentState;

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