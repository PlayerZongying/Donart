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

    // Start is called before the first frame update
    public CarStatus carStatus1;
    public CarStatus carStatus2;

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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTMPDisplay();
    }

    void UpdateTMPDisplay()
    {
        _uiManager.playerStatus1.text = $"Progress\n" +
                                        $"{carStatus1.progressInDegree: 0.0}\u00B0\n" +
                                        $"\n" +
                                        $"Rounds\n" +
                                        $"{carStatus1.rounds}";

        _uiManager.playerStatus2.text = $"Progress\n" +
                                        $"{carStatus2.progressInDegree: 0.0}\u00B0\n" +
                                        $"\n" +
                                        $"Rounds\n" +
                                        $"{carStatus2.rounds}";
    }
}