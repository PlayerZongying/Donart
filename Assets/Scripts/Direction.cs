using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public enum GuidingDirction
    {
        ClockWise,
        CounterClockWise
    }

    public GuidingDirction guidingDirction;
    public float movingSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (guidingDirction == GuidingDirction.CounterClockWise)
        {
            transform.position = new Vector3(TorusTrack.R, 0, 0);
        }
        else
        {
            transform.position = new Vector3( - TorusTrack.R, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float positionPhase;
        float theta;
        Vector3 targetRight;

        float rotationPhase;
        if (guidingDirction == GuidingDirction.CounterClockWise)
        {
            positionPhase = 0;
            theta = Time.time * movingSpeed + positionPhase;
            targetRight = transform.position - TorusTrack.Instance.transform.position;
        }
        else
        {
            positionPhase = Mathf.PI;
            theta = - Time.time * movingSpeed + positionPhase;
            targetRight = TorusTrack.Instance.transform.position - transform.position;
        }
        transform.position = new Vector3(TorusTrack.R * Mathf.Cos(theta), 0, TorusTrack.R * Mathf.Sin(theta));
        transform.right = targetRight;


    }
}