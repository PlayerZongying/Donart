using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarStatus : MonoBehaviour
{
    public enum TrackDirection
    {
        CounterClockWise,
        ClockWise,
    }

    public TrackDirection trackDirection;
    public Vector3 initHorizonPos;
    private float currentDegree = 0;
    private float lastDegree = 0;
    public int rounds = 0;
    public float progressInDegree = 0;

    // Start is called before the first frame update
    void Start()
    {
        initHorizonPos = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressInDegree();
    }

    private void UpdateProgressInDegree()
    {
        Vector3 curHorizonPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 axis;
        if (trackDirection == TrackDirection.CounterClockWise)
        {
            axis = Vector3.up;
        }
        else
        {
            axis = -Vector3.up;
        }

        // currentDegree range from 0 to 360 degree
        currentDegree = Vector3.SignedAngle(curHorizonPos, initHorizonPos, axis);
        if (currentDegree < 0)
        {
            currentDegree += 360;
        }
        
        if (lastDegree - currentDegree > 270)
        {
            rounds++;
        }
        else if(currentDegree - lastDegree > 270)
        {
            rounds--;
        }
        
        progressInDegree = currentDegree;
        
        lastDegree = currentDegree;
    }
}