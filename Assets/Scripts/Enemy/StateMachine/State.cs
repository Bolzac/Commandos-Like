using UnityEngine;

public abstract class State<T>: ScriptableObject where T : MonoBehaviour
{
    protected T Runner;
    public virtual void Init(T parent)
    {
        Runner = parent;
    }
    
    public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    public virtual void Exit(){}
}