using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAI = false;
    public AIInput aiInput;
    public CarController carController;
    public float InputLerpSpeed = 7;
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
        if (isAI)
        {
            targetInputVector = aiInput.GetAIInput();
        }

        inputVector = Vector2.Lerp(inputVector, targetInputVector, Time.deltaTime * InputLerpSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carController.SetInputVector(inputVector);
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (isAI) return;
        float inputValue = context.ReadValue<float>();
        targetInputVector.y = inputValue;
        // print($"Accelerate input: {inputValue}");
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        if (isAI) return;
        float inputValue = context.ReadValue<float>();
        targetInputVector.x = inputValue;
        // print($"Steer input: {inputValue}");
    }
}