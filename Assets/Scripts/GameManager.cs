using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameSettings gameSettings;
    public float time;

    public bool isStarted = false;
    public bool isFinished = false;
    public int countDownTime = 3;

    
    public CarController carController1;
    public CarController carController2;
    public CarStatus carStatus1;
    public CarStatus carStatus2;

    public struct Result
    {
        public string playerName;
        public float time;
        public Color color;

        public Result(string _playerName, float _time, Color _color)
        {
            playerName = _playerName;
            time = _time;
            color = _color;
        }
    }

    public List<Result> results;
    
    public int winningRounds = 3;

    private UIManager _uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _uiManager = UIManager.Instance;
        gameSettings.SetGame();
        
        if (gameSettings.isSinglePlayer)
        {
            SetGameForSinglePlayer();
        }
        
        results = new List<Result>();
        StartCoroutine(CountDown());
        carController1.Freeze();
        carController2.Freeze();
    }

    // Update is called once per frame
    void Update()
    {
        isFinished = carStatus1.isCompelete && carStatus2.isCompelete;
        if (isStarted && !isFinished)
        {
            time += Time.deltaTime;
        }
    }

    public void AddResult(string playerName, float time, Color color)
    {
        Result result = new Result(playerName, time, color);
        results.Add(result);
    }

    IEnumerator CountDown()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countDownTime--;
        }

        isStarted = true;
        carController1.UnFreeze();
        carController2.UnFreeze();
        StartCoroutine(_uiManager.panelReadyFading());
    }

    void SetGameForSinglePlayer()
    {
        CameraManager cameraManager = CameraManager.instance;
        bool isNightHuman = (Random.Range(0f, 1f) <= 0.5);
        print(isNightHuman);
        
        cameraManager.CameraForNight.gameObject.SetActive(isNightHuman);
        cameraManager.CameraForDay.gameObject.SetActive(!isNightHuman);

        carStatus1.gameObject.GetComponent<CarInputHandler>().isAI = !isNightHuman;
        carStatus2.gameObject.GetComponent<CarInputHandler>().isAI = isNightHuman;

    }
}