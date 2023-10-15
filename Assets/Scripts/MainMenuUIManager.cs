using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public GameSettings gameSettings;

    public Toggle chocolateBump;
    public Toggle melonSlide;
    public Toggle cherryBoom;

    public TextMeshProUGUI donutNumber;

    public Button donutMinus;
    public Button donutPlus;

    public Toggle singlePlayer;
    public Toggle doublePlayer;

    // Start is called before the first frame update
    void Start()
    {
        // gameSettings.ResetGameSettings();
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void SaveBumpActive(bool state)
    {
        gameSettings.hasBump = state;
    }

    public void SaveSlideActive(bool state)
    {
        gameSettings.hasSlide = state;
    }

    public void SaveBoomActive(bool state)
    {
        gameSettings.hasBoom = state;
    }

    public void SetBumpsActive(bool state)
    {
        BumpManager.Instance.gameObject.SetActive(state);
    }

    public void SetSlidesActive(bool state)
    {
        AcceleratorManager.Instance.gameObject.SetActive(state);
    }

    public void SetBoomsActive(bool state)
    {
        BoomManager.Instance.gameObject.SetActive(state);
    }

    public void SetSinglePlayer(bool isSinglePlayer)
    {
        gameSettings.isSinglePlayer = isSinglePlayer;
    }

    public void SetDoublePlayer(bool isDoublePlayer)
    {
        gameSettings.isSinglePlayer = !isDoublePlayer;
    }

    public void LoadGameScene()
    {
        if (gameSettings.isSinglePlayer)
        {
            SceneManager.LoadScene("SinglePlayer");
        }
        else
        {
            SceneManager.LoadScene("DoublePlayer");
        }
    }

    void InitUI()
    {
        chocolateBump.isOn = gameSettings.hasBump;
        melonSlide.isOn = gameSettings.hasSlide;
        cherryBoom.isOn = gameSettings.hasBoom;

        SetDonutPanel();

        singlePlayer.isOn = gameSettings.isSinglePlayer;
        doublePlayer.isOn = !gameSettings.isSinglePlayer;
    }

    public void ChangeDonutNumber(int i)
    {
        gameSettings.winningRounds += i;
        SetDonutPanel();
    }

    void SetDonutPanel()
    {
        gameSettings.winningRounds =
            Mathf.Clamp(gameSettings.winningRounds, gameSettings.minRound, gameSettings.maxRound);
        if (gameSettings.winningRounds == gameSettings.minRound)
        {
            donutMinus.interactable = false;
        }
        else if (gameSettings.winningRounds == gameSettings.maxRound)
        {
            donutPlus.interactable = false;
        }
        else
        {
            donutMinus.interactable = true;
            donutPlus.interactable = true;
        }

        donutNumber.text = gameSettings.winningRounds.ToString();
    }
}