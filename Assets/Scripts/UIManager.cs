using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI playerStatus1;
    public TextMeshProUGUI playerStatus2;
    public GameObject panelPause;

    private GameManager _gameManager;

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
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        panelPause.SetActive(!panelPause.activeSelf);
        Time.timeScale = panelPause.activeSelf ? 0 : 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        _gameManager.carStatus1.ResetCarStatus();
        _gameManager.carStatus2.ResetCarStatus();
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
