using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CarInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CarController carController;
    public float InputLerpSpeed = 5;
    Vector2 inputVector = Vector2.zero;
    Vector2 targetInputVector = Vector2.zero;
    void Start()
    {
        
    }

    private void Update()
    {
        UpdateInputVector();
    }

    void UpdateInputVector()
    {
        inputVector = Vector2.Lerp(inputVector, targetInputVector, Time.deltaTime * InputLerpSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carController.SetInputVector(inputVector);
    }
    
    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        targetInputVector.y = inputValue;
        // print($"Accelerate input: {inputValue}");
    }
    
    public void OnSteer(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        targetInputVector.x = inputValue;
        // print($"Steer input: {inputValue}");
    }
}
