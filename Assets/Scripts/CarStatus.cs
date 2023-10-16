using Unity.Mathematics;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    public enum TrackDirection
    {
        CounterClockWise,
        ClockWise,
    }

    [Header("Data for Display")] 
    public string carName;
    public TrackDirection trackDirection;
    public int rounds = 0;
    public float progressInDegree = 0;
    public bool isCompelete = false;
    public Color carColor;

    [Header("Data for Calculation")] 
    private Vector3 initPos;
    private Quaternion initRot;
    private Vector3 initHorizonPos;
    private float currentDegree = 0;
    private float lastDegree = 0;


    public void Init()
    {
        initPos = new Vector3(transform.position.x, 0, transform.position.z).normalized * TorusTrack.R;
        initHorizonPos = new Vector3(initPos.x, 0, initPos.z);
        initRot = quaternion.identity;
        ResetCarStatus();
    }

    public void ResetCarStatus()
    {
        transform.position = initPos;
        transform.rotation = initRot;
        currentDegree = 0;
        lastDegree = 0;
        progressInDegree = 0;
        rounds = 0;
        isCompelete = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompelete) return;
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
        else if (currentDegree - lastDegree > 270)
        {
            rounds--;
        }

        progressInDegree = currentDegree;

        lastDegree = currentDegree;

        if (rounds == GameManager.Instance.winningRounds)
        {
            isCompelete = true;
            progressInDegree = 0;
            rounds = GameManager.Instance.winningRounds;

            GameManager.Instance.AddResult(carName, GameManager.Instance.time, carColor);
        }
    }
}