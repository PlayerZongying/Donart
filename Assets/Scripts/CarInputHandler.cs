using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CarController carController;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        print($"x:{x}, y:{y}");
        carController.SetInputVector(new Vector2(x, y));
    }
}
