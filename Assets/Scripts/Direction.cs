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
        transform.rotation = RotationTowardsCorrectDriveDirection(transform, guidingDirction);
    }

    public static Quaternion RotationTowardsCorrectDriveDirection(Transform transform, GuidingDirction guidingDirction)
    {
        Vector3 targetRight;
        Vector3 targetUp;

        if (guidingDirction == GuidingDirction.CounterClockWise)
        {
            targetRight = transform.position - TorusTrack.Instance.transform.position;
        }
        else
        {
            targetRight = TorusTrack.Instance.transform.position - transform.position;
        }
        
        targetUp = TorusTrack.Instance.transform.up;
        
        Vector3 targetForward = Vector3.Cross(targetRight, targetUp);
        Quaternion rotation = Quaternion.LookRotation(targetForward,targetUp);

        return rotation;
    }
}