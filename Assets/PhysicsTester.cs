using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTester : MonoBehaviour
{
    public Rigidbody TestRb;
    public Vector3 Force;
    public bool isRunning = false;

    private Vector3 initPos;

    private Vector3 velocityLast = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        initPos = TestRb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestRb.transform.position = initPos;
            TestRb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isRunning = !isRunning;
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            TestRb.AddForce(Force);
        }


        Vector3 deltaVel = TestRb.velocity - velocityLast;
        Vector3 accel = deltaVel / Time.deltaTime;
        if (accel.magnitude > 0)
        {
            Debug.Log($"Accel magnitute: {accel.magnitude:0.00} | Accel direction: {accel.normalized}");
        }
        else
        {
            Debug.Log($"Accel magnitute: {accel.magnitude:0.00} | Accel direction: {Vector3.zero}");
        }

        velocityLast = TestRb.velocity;
    }
}