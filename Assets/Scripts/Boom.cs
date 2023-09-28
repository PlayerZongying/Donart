using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boom : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionForce;
    public float explosionRadius;
    public float upForce;
    public Vector2 denoteTimeRange;
    private float denoteTime;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     ExpoldeAndInpact();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb.CompareTag("Player"))
        {
            StartCoroutine(Denote());
        }
    }

    IEnumerator Denote()
    {
        float timePassed = 0;
        while (timePassed < denoteTime)
        {
            timePassed += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }

        ExpoldeAndInpact();
        
    }

    public void ExpoldeAndInpact()
    {
        // print("should explode");
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var collider in colliders)
        {
            // print(collider);
            Rigidbody rb = collider.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upForce);
            }
        }
    }

    public void ResetAtRandomPos()
    {
        
    }
}