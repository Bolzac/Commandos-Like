using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public DateTime createTime;
    
    public Member mainMember;
    public SerializableDictionary<string, bool> interactions;
    public SerializableDictionary<string, Vector3> memberPositions;
    public SerializableDictionary<string, bool> memberState; //Crouch or stand

    public GameData(DateTime date)
    {
        createTime = date;
        
        mainMember = null;
        interactions = new SerializableDictionary<string, bool>();
        memberPositions = new SerializableDictionary<string, Vector3>();
        memberState = new SerializableDictionary<string, bool>();
    }
}