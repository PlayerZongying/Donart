using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AcceleratorManager : MonoBehaviour
{
    public static AcceleratorManager Instance;
    public GameObject acceleratorPrefab;
    public int acceleratorCount = 3;
    public Accelerator[] accelerators;
    public Vector2 acceleratorAppearTimeRange;
    public Vector2 acceleratorDisappearTimeRange;

    public float rotateSpeed = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        accelerators = new Accelerator[acceleratorCount];
        for (int i = 0; i < acceleratorCount; i++)
        {
            GameObject newAcceleratorObject = Instantiate(acceleratorPrefab);
            newAcceleratorObject.transform.SetParent(transform);
            Accelerator accelerator = newAcceleratorObject.GetComponent<Accelerator>();
            accelerators[i] = accelerator;
            StartCoroutine(AcceleratorBehavior(accelerator));
        }
    }

    private void OnEnable()
    {
        foreach (var accelerator in accelerators)
        {
            StartCoroutine(AcceleratorBehavior(accelerator));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator AcceleratorBehavior(Accelerator accelerator)
    {
        
        float randomRotationAngle = Random.Range(0f, 360f);
        accelerator.transform.localEulerAngles = new Vector3(0, randomRotationAngle, 0);
        accelerator.transform.position = TorusTrack.Instance.transform.position + accelerator.transform.right * TorusTrack.R;

        float randomPhase = Random.Range(0f, 360f);
        accelerator.transform.localEulerAngles += new Vector3(0, 0, randomPhase);

        float randomApperTime = Random.Range(acceleratorAppearTimeRange.x, acceleratorAppearTimeRange.y);
        float randomDisapperTime = Random.Range(acceleratorDisappearTimeRange.x, acceleratorDisappearTimeRange.y);

        while (randomApperTime > 0)
        {
            randomApperTime -= Time.deltaTime;
            accelerator.transform.localEulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
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