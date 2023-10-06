using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Boom : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionForce;
    public float explosionRadius;
    public float upForce;
    public Vector2 denoteTimeRange;
    private float denoteTime;
    public Vector2 spawnTimeRange;
    private float spawnTime;
    
    public PositionOnTorus positionOnTorus;

    void Start()
    {
    }

    public void SetRandomPositionOnTorus()
    {
        float randomTheta = Random.Range(0f, 360f);
        float randomPhi = Random.Range(0f, 360f);
        SetPositiononOnTorus(randomTheta, randomPhi);
    }
    
    public void SetPositiononOnTorus(float thetaInDegree, float phiInDegree)
    {
        positionOnTorus.thetaInDegree = thetaInDegree;
        positionOnTorus.phiInDegree = phiInDegree;
        transform.position = TorusTrack.PositionOnTorusSurface(thetaInDegree, phiInDegree);
    }

    // Update is called once per frame
    void Update()
    {
        positionOnTorus.thetaInDegree += Time.deltaTime * 15 * (Mathf.PerlinNoise1D(Time.time));
        positionOnTorus.phiInDegree += Time.deltaTime * 15 * (Mathf.PerlinNoise1D(Time.time));
        SetPositiononOnTorus(positionOnTorus.thetaInDegree, positionOnTorus.phiInDegree);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb.CompareTag("Player"))
        {
            StartCoroutine( Denote());
        }
    }

    public IEnumerator Denote()
    {
        float timePassed = 0;
        while (timePassed < denoteTime)
        {
            timePassed += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
        ExpoldeAndInpact();
        
        gameObject.SetActive(false);
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

    private void OnEnable()
    {
        BoomManager.Instance.enabledBoomCount++;
    }
    
    private void OnDisable()
    {
        BoomManager.Instance.enabledBoomCount--;
    }

    public void ResetAtRandomPos()
    {
        
    }
}