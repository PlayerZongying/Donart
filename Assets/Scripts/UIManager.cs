using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Before Game Start")] public GameObject panelReady;
    public TextMeshProUGUI ReadyText;
    public float fadingTime = 1;

    [Header("Game Status")] public GameObject playerPanel1;
    public TextMeshProUGUI playerStatus1;
    public Slider progress1;

    public GameObject playerPanel2;
    public TextMeshProUGUI playerStatus2;
    public Slider progress2;
    public TextMeshProUGUI time;

    [Header("Pause Panel")] public GameObject panelPause;

    [Header("Result Panel")] public GameObject panelResult;
    public TextMeshProUGUI winningText;
    public GameObject ResultList;

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
        panelReady.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }

        DisplayCountDown();
        UpdateTMPDisplay();
        ShowResult();
    }

    public void TogglePausePanel()
    {
        if (_gameManager.isFinished) return;
        panelPause.SetActive(!panelPause.activeSelf);
        Time.timeScale = panelPause.activeSelf ? 0 : 1;
    }

    public void ResetGame()
    {
        _gameManager.time = 0;
        _gameManager.carStatus1.ResetCarStatus();
        _gameManager.carStatus2.ResetCarStatus();
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowResult()
    {
        panelResult.SetActive(_gameManager.isFinished);
        if (_gameManager.isFinished)
        {
            winningText.text = $"{_gameManager.results[0].playerName} Win!!!";
            winningText.color = _gameManager.results[0].color;
            TextMeshProUGUI[] resultTexts = ResultList.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < 2; i++)
            {
                resultTexts[i].text = $"{_gameManager.results[i].playerName} {_gameManager.results[i].time: 0.00}s";
                resultTexts[i].color = _gameManager.results[i].color;
            }
        }
    }

    void UpdateTMPDisplay()
    {
        playerPanel1.SetActive(!_gameManager.isFinished);
        if (!_gameManager.carStatus1.isCompelete)
        {
            playerStatus1.text = $"Rounds\n" + $"{_gameManager.carStatus1.rounds}/{_gameManager.winningRounds}";
            progress1.value = _gameManager.carStatus1.progressInDegree / 360;
        }
        else
        {
            playerStatus1.text = "Finish!";
            progress1.value = 1;
        }


        playerPanel2.SetActive(!_gameManager.isFinished);
        if (!_gameManager.carStatus2.isCompelete)
        {
            playerStatus2.text = $"Rounds\n" + $"{_gameManager.carStatus2.rounds}/{_gameManager.winningRounds}";
            progress2.value = _gameManager.carStatus2.progressInDegree / 360;
        }
        else
        {
            playerStatus2.text = "Finish";
            progress2.value = 1;
        }
        
        time.enabled = (!_gameManager.isFinished);
        time.text = $"{_gameManager.time:0.00}";
    }

    public void Rematch()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    void DisplayCountDown()
    {
        if (_gameManager.isStarted) return;
        ReadyText.text = _gameManager.countDownTime.ToString();
    }

    public IEnumerator panelReadyFading()
    {
        ReadyText.text = "GO!";
        float timePassed = 0;
        while (timePassed < fadingTime)
        {
            timePassed += Time.deltaTime;
            float t = 1 - timePassed / fadingTime;

            Image bgImage = panelReady.GetComponent<Image>();
            Color bgColor = bgImage.color;
            bgColor.a *= t;
            bgImage.color = bgColor;

            Color textColor = ReadyText.color;
            textColor.a *= t;
            ReadyText.color = textColor;

            yield return new WaitForEndOfFrame();
        }

        panelReady.SetActive(false);
    }
}