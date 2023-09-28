using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorManager : MonoBehaviour
{
    public GameObject acceleratorPrefab;
    public int acceleratorCount = 3;
    public GameObject[] accelerators;
    public Vector2 acceleratorAppearTimeRange;

    public Vector2 acceleratorDisappearTimeRange;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < acceleratorCount; i++)
        {
            GameObject newAccelerator = Instantiate(acceleratorPrefab);
            newAccelerator.transform.SetParent(transform);
            StartCoroutine(AcceleratorBehavior(newAccelerator));
        }
        // foreach (var accelerator in accelerators)
        // {
        //     
        // }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AcceleratorBehavior(GameObject accelerator)
    {
        float randomRotationAngle = Random.Range(0f, 360f);
        accelerator.transform.rotation = Quaternion.Euler(0, randomRotationAngle, 0);
        
        float randomApperTime = Random.Range(acceleratorAppearTimeRange.x, acceleratorAppearTimeRange.y);
        float randomDisapperTime = Random.Range(acceleratorDisappearTimeRange.x, acceleratorDisappearTimeRange.y);
        
        while (randomApperTime > 0)
        {
            randomApperTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        accelerator.gameObject.SetActive(false);
        
        while (randomDisapperTime > 0)
        {
            randomDisapperTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        accelerator.gameObject.SetActive(true);
        StartCoroutine(AcceleratorBehavior(accelerator));
    }
}