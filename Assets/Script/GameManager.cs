using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Value { get; set; } = 0;
    public static int AllLevel { get; set; } = 12;
    public static int FinishLevel { get; set ; } = 1;
    public bool CanMove = false;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private List<WinnerManager> WinnerManagers = new List<WinnerManager>();

    private void Start()
    {
        SceneManager.LoadScene(FinishLevel);
    }

    private void Update()
    {
        if (CanMove)
        {
            WinThisLevel();
            CanMove = false;
        }
    }

    public void WinThisLevel()
    {
        
        if (FinishLevel<=AllLevel)
        {
            SceneManager.LoadScene(FinishLevel);
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
