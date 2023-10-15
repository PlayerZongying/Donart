using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public bool hasBump = false;
    public bool hasSlide = false;
    public bool hasBoom = false;

    public int winningRounds = 3;
    public int minRound = 1;
    public int maxRound = 10;

    public bool isSinglePlayer = false;
    

    public void ResetGameSettings()
    {
        hasBump = false;
        hasSlide = false;
        hasBoom = false;
        
    }

    public void SetGame()
    {
        BumpManager.Instance.gameObject.SetActive(hasBump);
        AcceleratorManager.Instance.gameObject.SetActive(hasSlide);
        BoomManager.Instance.gameObject.SetActive(hasBoom);
        GameManager.Instance.winningRounds = winningRounds;
    }
}
