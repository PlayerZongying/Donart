using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameSettings gameSettings;
    public float time;

    public bool isFinished = false;

    // Start is called before the first frame update
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

    public int winningRounds = 10;

    public TextMeshPro carStatusTMP1;
    public TextMeshPro carStatusTMP2;

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
        results = new List<Result>();
    }

    // Update is called once per frame
    void Update()
    {
        isFinished = carStatus1.isCompelete && carStatus2.isCompelete;
        if (!isFinished)
        {
            time += Time.deltaTime;
        }
    }

    public void AddResult(string playerName, float time, Color color)
    {
        Result result = new Result(playerName,time, color);
        results.Add(result);
    }
}