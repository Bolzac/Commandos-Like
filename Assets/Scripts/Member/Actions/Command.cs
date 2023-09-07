using UnityEngine;

public abstract class Command<T> where T : MonoBehaviour
{
        protected T runner;

        protected Command(T member)
        {
                runner = member;
        }
        public abstract void Start();
        public abstract bool IsFinished();
}