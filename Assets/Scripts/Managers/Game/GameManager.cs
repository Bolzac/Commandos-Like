using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("SubManagers")] 
    public LevelManager levelManager;
    public SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);

        levelManager = GetComponentInChildren<LevelManager>();
        soundManager = GetComponentInChildren<SoundManager>();
    }
}