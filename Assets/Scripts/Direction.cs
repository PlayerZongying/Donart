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

    void Update()
    {
        float positionPhase;
        float theta;
        Vector3 targetRight;
        Vector3 targetUp;

        float rotationPhase;
        if (guidingDirction == GuidingDirction.CounterClockWise)
        {
            // positionPhase = 0;
            // theta = Time.time * movingSpeed + positionPhase;
            targetRight = transform.position - TorusTrack.Instance.transform.position;
            targetUp = Vector3.up;
        }
        else
        {
            // positionPhase = Mathf.PI;
            // theta = - Time.time * movingSpeed + positionPhase;
            targetRight = TorusTrack.Instance.transform.position - transform.position;
            targetUp = Vector3.up;
        }
        // transform.position = new Vector3(TorusTrack.R * Mathf.Cos(theta), 0, TorusTrack.R * Mathf.Sin(theta));
        transform.right = targetRight;
        transform.forward = Vector3.Cross(targetRight, targetUp);



    }
}