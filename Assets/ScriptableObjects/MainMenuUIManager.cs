using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public GameSettings gameSettings;
    // Start is called before the first frame update
    void Start()
    {
        gameSettings.ResetGameSettings();
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

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    
}
