using UnityEngine;

public class Child : MonoBehaviour
{
    public Parent runner;

    public void Init(Parent parent)
    {
        runner = parent;
    }
}