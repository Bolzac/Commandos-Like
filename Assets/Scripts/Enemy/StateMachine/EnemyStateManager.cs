using System;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyStateManager : MonoBehaviour
    {
        public Enemy enemy;
        public List<State<Enemy>> states;
        private Dictionary<Type, State<Enemy>> _statesByTypes;
        public State<Enemy> currentState;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            _statesByTypes = new Dictionary<Type, State<Enemy>>();
            foreach (var state in states)
            {
                _statesByTypes.Add(state.GetType(),state);
                state.Init(enemy);
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
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }