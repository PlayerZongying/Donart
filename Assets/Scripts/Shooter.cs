using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform shooterPoint;

    public Rigidbody projectileRb;

    public Rigidbody playerRb;

    public bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        

        Vector3 dir = playerRb.transform.position - projectileRb.transform.position;
        projectileRb.AddForce(dir * 1000, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            projectileRb.position = shooterPoint.position;
            projectileRb.velocity = Vector3.zero;
        }
    }
}