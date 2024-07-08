using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WinnerManager Instance { get; private set; }
    public GameManager gameManager;
    private GameObject[] EndPoss;
    public int NowCount = 0;
    public bool isWIn = true;
    public GameObject winMessage;
    public float winTime = 1.0f;
    private float timeSet = 0;
    public bool winEnd = false;
    public AudioSource winAudio;
    public bool isPlay = false;
    private void Awake()
    {
        Instance = this;
        NowCount = 0;
        EndPoss = GameObject.FindGameObjectsWithTag("Winner");
    }
    private void Start()
    {
        isWIn = false;
        gameManager = GameManager.Instance;
        timeSet = 0;
        winAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //胜利，开始播动效并开始计时
        if (NowCount == EndPoss.Length)
        {
            isWIn = true;
            /*if (isWIn)
            {
                SceneManager.LoadScene(0);
                GameManager.FinishLevel++;
                gameManager.CanMove = true;
                isWIn = false;
            }*/
        }
        //计时，禁止输入
        if (isWIn == true)
        {
            if (timeSet <= winTime)
            {
                if (!isPlay)
                {
                    winAudio.Play();
                    isPlay = true;
                }
                
                timeSet += Time.deltaTime;
                //播放动效
                Animator ani = winMessage.GetComponent<Animator>();
                ani.SetBool("isWin", true);
                
            }
            else
            {
                
                //结束后切关卡
                timeSet = 0;
                isWIn = false;
                GameManager.FinishLevel++;
                SceneManager.LoadScene(0);
                gameManager.CanMove = true;
                isWIn = false;
            }

        }

        
        
        
        
    }
}
