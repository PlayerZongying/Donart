using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")] [SerializeField]
    private Rigidbody carRigidBody;

    [SerializeField] private Vector3 CenterOfMass;

    [Header("Car Suspension")] [SerializeField]
    private Transform[] wheelTransforms;

    [SerializeField] private float suspensionDist = 0.5f;
    [SerializeField] private float suspensionStrength = 100;
    [SerializeField] private float suspensionDamping = 15;

    [Header("Car Steering")] [Range(0f, 1f)] [SerializeField]
    private float wheelGripFactor = 1f;

    [SerializeField] private AnimationCurve wheelGripFactorCurve;
    [SerializeField] private float maxRotationAngle = 30;
    [SerializeField] private float wheelMass = 10f;

    [Header("Car Acceleration")] [SerializeField]
    private float accelInput;

    [SerializeField] private AnimationCurve accelrationInputCurve;
    [SerializeField] private float maxAccelation;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float normalizedForwardSpeed;
    [SerializeField] private float frictionFactor = 0;
    // [SerializeField] private float accelarationFactor = 30f;

    private float steeringInput = 0;

    private float rotationAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        carRigidBody.centerOfMass = CenterOfMass;
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
                float normalizedSpeed = carRigidBody.velocity.magnitude / maxSpeed;
                wheelGripFactor = wheelGripFactorCurve.Evaluate(normalizedSpeed);
                float desiredVelChange = -steeringVel * wheelGripFactor;

                float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

                carRigidBody.AddForceAtPosition(steeringDir * wheelMass * desiredAccel, wheelTransform.position);
                Debug.DrawRay(wheelTransform.position, steeringDir * wheelMass * desiredAccel, Color.red);

                // acceleration / braking

                Vector3 accelDir = wheelTransform.forward;
                float carSpeed = Vector3.Dot(transform.forward, carRigidBody.velocity);
                // normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / maxSpeed);
                normalizedForwardSpeed = carSpeed / maxSpeed;
                
                float evaluatedAccelInput = accelInput * accelrationInputCurve.Evaluate(Mathf.Sign(accelInput) * normalizedForwardSpeed);
                
                // print(evaluatedAccelInput);
                float availableTorque = evaluatedAccelInput * maxAccelation;

                float frictionMagnitute = carRigidBody.mass * frictionFactor;
                Vector3 friction = -accelDir * frictionMagnitute;
                if (Vector3.Dot(carRigidBody.velocity, accelDir) < 0)
                {
                    friction *= -1;
                }

                carRigidBody.AddForceAtPosition(accelDir * availableTorque + friction, wheelTransform.position);
            }
        }

        
        ApplySteering();

        // print(carRigidBody.velocity.magnitude);
    }
    void ApplySteering()
    {

        for (int i = 0; i < 2; i++)
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
        transform.position += Vector3.up * 5;

        Vector3 targetUp = Vector3.up;
        Vector3 targetRight = Vector3.Cross(targetUp, transform.forward);
        Vector3 targetForward = Vector3.Cross(targetRight, targetUp);

        transform.rotation = Quaternion.LookRotation(targetForward, targetUp);
        carRigidBody.velocity = Vector3.zero;
        carRigidBody.angularVelocity = Vector3.zero;
    }
}