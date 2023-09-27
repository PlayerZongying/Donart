using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CarStatus carStatus1;
    public CarStatus carStatus2;

    public TextMeshPro carStatusTMP1;
    public TextMeshPro carStatusTMP2;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTMPDisplay();
    }

    void UpdateTMPDisplay()
    {
        carStatusTMP1.text = $"Progress\n" +
                             $"{carStatus1.progressInDegree : 0.0}\u00B0\n"+
                             $"\n"+
                             $"Rounds\n" +
                             $"{carStatus1.rounds}";
        
        carStatusTMP2.text = $"Progress\n" +
                             $"{carStatus2.progressInDegree : 0.0}\u00B0\n"+
                             $"\n"+
                             $"Rounds\n" +
                             $"{carStatus2.rounds}";
    }
}