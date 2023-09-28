using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float forceMagnitute = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        print(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb.CompareTag("Player"))
        {
            Accelerate(rb);
            //print("Player Touched!");
        }
    }

    private void Accelerate(Rigidbody rb)
    {
        Vector3 forceDir = rb.velocity.magnitude != 0 ? rb.velocity.normalized : rb.transform.forward;
        Vector3 force = forceDir * forceMagnitute;
        rb.AddForce(force,ForceMode.Impulse);
    }
}
