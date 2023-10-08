using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BumpManager : MonoBehaviour
{
    public static BumpManager Instance;
    public GameObject bumpPrefab;
    public int bumpCount = 20;
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
        BumpManagerInit();
    }

    public void BumpManagerInit()
    {
        for (int i = 0; i < bumpCount; i++)
        {
            GameObject newBumpGameObject = Instantiate(bumpPrefab);
            float randomTheta = Random.Range(0f, 360f);
            float randomPhi = Random.Range(0f, 360f);
            newBumpGameObject.transform.position = TorusTrack.PositionOnTorusSurface(randomTheta, randomPhi);
            newBumpGameObject.transform.rotation = TorusTrack.OrientaionOnTorusSurface(randomTheta, randomPhi);
            newBumpGameObject.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
