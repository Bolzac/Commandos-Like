using System.Collections.Generic;
using UnityEngine;
using System;

public class CommandManager : MonoBehaviour
{
    public Queue<Command<Member>> commandQueue = new Queue<Command<Member>>();
    public Command<Member> currentCommand;

    public Member member;

    public void AddCommand(Command<Member> command)
    {
        commandQueue.Enqueue(command);
    }

    public void ClearCommands()
    {
        commandQueue.Clear();
    }

    public void StopQueue()
    {
        commandQueue.Clear();
        currentCommand = null;
    }

    private void Update()
    {
        if (commandQueue.Count > 0 && currentCommand == null )
        {
            currentCommand = commandQueue.Dequeue();
            currentCommand.Start();
        }
        else if(currentCommand != null && currentCommand.IsFinished())
        {
            currentCommand = null;
        }
    }
}