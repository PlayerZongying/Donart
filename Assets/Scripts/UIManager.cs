using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Before Game Start")] 
    public GameObject panelReady;
    public TextMeshProUGUI ReadyText;
    
    [Header("Game Status")] 
    public TextMeshProUGUI playerStatus1;
    public TextMeshProUGUI playerStatus2;
    public TextMeshProUGUI time;
    
    [Header("Pause Panel")] 
    public GameObject panelPause;
    
    [Header("Result Panel")] 
    public GameObject panelResult;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }


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
        playerStatus1.enabled = (!_gameManager.isFinished);
        playerStatus1.text = $"Progress\n" +
                             $"{_gameManager.carStatus1.progressInDegree: 0.0}\u00B0\n" +
                             $"\n" +
                             $"Rounds\n" +
                             $"{_gameManager.carStatus1.rounds}";


        playerStatus2.enabled = (!_gameManager.isFinished);
        playerStatus2.text = $"Progress\n" +
                             $"{_gameManager.carStatus2.progressInDegree: 0.0}\u00B0\n" +
                             $"\n" +
                             $"Rounds\n" +
                             $"{_gameManager.carStatus2.rounds}";

        time.enabled = (!_gameManager.isFinished);
        time.text = $"{_gameManager.time:0.00}";
    }

    public void Rematch()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}