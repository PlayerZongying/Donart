using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")] public Rigidbody carRigidBody;

    [Header("Car Suspension")] public Transform[] wheelTransforms;
    public float suspensionDist = 0.5f;
    public float suspensionStrength = 100;
    public float suspensionDamping = 15;

    [Header("Car Steering")] [Range(0f, 1f)]
    public float wheelGripFactor = 1f;

    public AnimationCurve wheelGripFactorCurve;
    public float maxRotationAngle = 30;
    public float wheelMass = 10f;

    [Header("Car Acceleration")] 
    public float accelInput;
    public float accelFactor;
    public float maxSpeed;
    public float frictionFactor = 0;
    public float accelarationFactor = 30f;

    public float turnFactor = 2.5f;

    private float accelarationInput = 0;

    private float steeringInput = 0;

    private float rotationAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCar();
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform wheelTransform in wheelTransforms)
        {
            RaycastHit hit;
            if (Physics.Raycast(wheelTransform.position, -wheelTransform.up, out hit, suspensionDist,
                    LayerMask.GetMask("Default")))
            {
                // suspension
                Debug.DrawRay(wheelTransform.position, -wheelTransform.up * hit.distance, Color.yellow);
                float offset = suspensionDist - hit.distance;
                float velocity = Vector3.Dot(wheelTransform.up, carRigidBody.GetPointVelocity(wheelTransform.position));
                float force = (offset * suspensionStrength) - (velocity * suspensionDamping);
                carRigidBody.AddForceAtPosition(wheelTransform.up * force, wheelTransform.position);
                Debug.DrawRay(wheelTransform.position, wheelTransform.up * force, Color.green);

                // steering
                Vector3 steeringDir = wheelTransform.right;

                Vector3 wheelWorldVel = carRigidBody.GetPointVelocity(wheelTransform.position);
                float steeringVel = Vector3.Dot(steeringDir, wheelWorldVel);
                float normalizedSpped = carRigidBody.velocity.magnitude / maxSpeed;
                wheelGripFactor = wheelGripFactorCurve.Evaluate(normalizedSpped);
                float desiredVelChange = -steeringVel * wheelGripFactor;

                float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

                carRigidBody.AddForceAtPosition(steeringDir * wheelMass * desiredAccel, wheelTransform.position);
                Debug.DrawRay(wheelTransform.position, steeringDir * wheelMass * desiredAccel, Color.red);

                // acceleration / braking

                Vector3 accelDir = wheelTransform.forward;
                float carSpeed = Vector3.Dot(transform.forward, carRigidBody.velocity);
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / maxSpeed);
                float availableTorque = (1 - normalizedSpeed) * accelInput * accelFactor;
                float frictionMagnitute = carRigidBody.mass * Vector3.Dot(transform.up, Vector3.up) * frictionFactor;
                Vector3 friction = - accelDir * frictionMagnitute;
                if (Vector3.Dot(carRigidBody.velocity, accelDir) < 0)
                {
                    friction *= -1;
                }
                carRigidBody.AddForceAtPosition(accelDir * availableTorque + friction, wheelTransform.position);
                
                
                
            }
        }

        // ApplyEngineForce();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        Vector3 engineForceVector = transform.forward * accelarationInput * accelarationFactor;
        carRigidBody.AddForce(engineForceVector);
    }

    void ApplySteering()
    {
        // rotationAngle += steeringInput * turnFactor;
        // Quaternion steeredRotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
        // carRigidBody.MoveRotation(steeredRotation);

        for(int i = 0;i < 2; i++)
        {
            rotationAngle = steeringInput * maxRotationAngle;
            // Quaternion steeredRotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
            wheelTransforms[i].localEulerAngles = new Vector3(0, rotationAngle, 0);
        }
    }

    public void SetInputVector(Vector2 inputVector)
    {
        accelInput = inputVector.y;
        steeringInput = inputVector.x;
    }

    private void ResetCar()
    {
        transform.position = Vector3.zero;
        transform.position +=  Vector3.up * 5;

        Vector3 targetUp = Vector3.up;
        Vector3 targetRight = Vector3.Cross(targetUp, transform.forward);
        Vector3 targetForward = Vector3.Cross(targetRight, targetUp);
        
        transform.rotation = Quaternion.LookRotation(targetForward,targetUp);
        carRigidBody.velocity = Vector3.zero;
        carRigidBody.angularVelocity = Vector3.zero;
    }
}