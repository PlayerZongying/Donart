using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoomManager : MonoBehaviour
{
    public static BoomManager Instance;
    public GameObject BoomPrefab;
    public int desiredBoomCount = 20;
    public int enabledBoomCount = 0;
    public Vector2 denoteTimeRange;
    public Boom[] boomPool = new Boom[30];
    private int poolIndex = 0;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        for (int i = 0; i < boomPool.Length; i++)
        {
            GameObject newBoomGameObject = Instantiate(BoomPrefab);
            Boom newBoom = newBoomGameObject.GetComponent<Boom>();
            boomPool[i] = newBoom;
            newBoom.transform.SetParent(transform);
            newBoomGameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (enabledBoomCount < desiredBoomCount)
        {
            Boom boomToEnable = FindNextDisabledBoom();
            boomToEnable.gameObject.SetActive(true);
            boomToEnable.SetRandomPositionOnTorus();
        }
    }

    private Boom FindNextDisabledBoom()
    {
        while (boomPool[poolIndex].gameObject.activeSelf)
        {
            poolIndex = poolIndex == boomPool.Length - 1 ? 0 : poolIndex + 1;
        }
        return boomPool[poolIndex];
    }
    
    public IEnumerator Denote(Boom boom)
    {
        float denoteTime = Random.Range(denoteTimeRange.x, denoteTimeRange.y);
        
        float timePassed = 0;
        while (timePassed < denoteTime)
        {
            timePassed += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }

        boom.ExpoldeAndInpact();
        boom.SetRandomPositionOnTorus();
    }
}