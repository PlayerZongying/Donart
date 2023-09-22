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
            if (Physics.Raycast(wheelTransform.position, -wheelTransform.up, out hit, Mathf.Infinity,
                    LayerMask.GetMask("Default")))
            {
                Debug.DrawRay(wheelTransform.position, -wheelTransform.up * hit.distance, Color.green);
                float offset = suspensionDist - hit.distance;
                float velocity = Vector3.Dot(wheelTransform.up, carRigidBody.GetPointVelocity(wheelTransform.position));
                float force = (offset * suspensionStrength) - (velocity * suspensionDamping);
                carRigidBody.AddForceAtPosition(wheelTransform.up * force, wheelTransform.position);
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
        accelarationInput = inputVector.y;
        steeringInput = inputVector.x;
    }
}