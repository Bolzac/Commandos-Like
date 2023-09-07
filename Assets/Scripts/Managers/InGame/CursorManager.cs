using System;
using UnityEngine;
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    [SerializeField] private Texture2D defaultCursor;

    [Header("Cursor Icons")] 
    [SerializeField] private Texture2D member;
    [SerializeField] private Texture2D enemy;
    [SerializeField] private Texture2D npc;
    [SerializeField] private Texture2D other;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ReturnDefaultCursor();
    }

    public void ReturnDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor,Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetCursor(HoverState hoverState)
    {
        switch (hoverState)
        {
            case HoverState.Member:
                Cursor.SetCursor(member,Vector2.zero, CursorMode.ForceSoftware);
                break;
            case HoverState.Enemy:
                Cursor.SetCursor(enemy,Vector2.zero, CursorMode.ForceSoftware);
                break;
            case HoverState.NPC:
                Cursor.SetCursor(npc,Vector2.zero, CursorMode.ForceSoftware);
                break;
            case HoverState.Other:
                Cursor.SetCursor(other,Vector2.zero, CursorMode.ForceSoftware);
                break;
        }
    }
}