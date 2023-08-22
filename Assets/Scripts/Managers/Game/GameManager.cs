using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isOverUI;
    public float waitTime;

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

    private void Start()
    {
        StartCoroutine(CheckForOverUI());
    }

    private IEnumerator CheckForOverUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }
    }
}