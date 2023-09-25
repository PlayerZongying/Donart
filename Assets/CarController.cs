using System;
using System.Collections;
using System.Collections.Generic;
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

    public float wheelMass = 10f;

    [Header("Car Acceleration")] 
    public float accelInput;
    public float accelFactor;
    public float maxSpeed;
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
                Debug.DrawRay(wheelTransform.position, -wheelTransform.up * hit.distance, Color.green);
                float offset = suspensionDist - hit.distance;
                float velocity = Vector3.Dot(wheelTransform.up, carRigidBody.GetPointVelocity(wheelTransform.position));
                float force = (offset * suspensionStrength) - (velocity * suspensionDamping);
                carRigidBody.AddForceAtPosition(wheelTransform.up * force, wheelTransform.position);

                // steering
                Vector3 steeringDir = wheelTransform.right;

                Vector3 wheelWorldVel = carRigidBody.GetPointVelocity(wheelTransform.position);
                float steeringVel = Vector3.Dot(steeringDir, wheelWorldVel);
                float desiredVelChange = -steeringVel * wheelGripFactor;

                float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

                carRigidBody.AddForceAtPosition(steeringDir * wheelMass * desiredAccel, wheelTransform.position);
                Debug.DrawRay(wheelTransform.position, steeringDir * wheelMass * desiredAccel, Color.red);

                // acceleration / braking

                Vector3 accelDir = wheelTransform.forward;
                float carSpeed = Vector3.Dot(transform.forward, carRigidBody.velocity);
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / maxSpeed);
                float availableTorque = (1 - normalizedSpeed) * accelInput * accelFactor;
                carRigidBody.AddForceAtPosition(accelDir * availableTorque, wheelTransform.position);
                
                
                
            }
        }

        ApplyEngineForce();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        Vector3 engineForceVector = transform.forward * accelarationInput * accelarationFactor;
        carRigidBody.AddForce(engineForceVector);
    }

    void ApplySteering()
    {
        rotationAngle += steeringInput * turnFactor;
        Quaternion steeredRotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
        carRigidBody.MoveRotation(steeredRotation);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        accelInput = inputVector.y;
        steeringInput = inputVector.x;
    }
}