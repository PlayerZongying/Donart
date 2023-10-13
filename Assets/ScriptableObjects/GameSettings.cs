using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public bool hasBump = false;
    public bool hasSlide = false;
    public bool hasBoom = false;

    public int winningRounds = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
