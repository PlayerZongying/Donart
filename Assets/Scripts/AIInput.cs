using UnityEngine;

public class AIInput : MonoBehaviour
{
    // Start is called before the first frame update

    public Direction.GuidingDirction guidingDirction = Direction.GuidingDirction.CounterClockWise;
    private Vector2 aiInputValue = Vector2.zero;
    private Vector3 baseForward;
    private Vector3 targetForward;
    private Quaternion targetRotation = Quaternion.identity;

    void Start()
    {
        baseForward = TorusTrack.Instance.transform.forward;
        targetForward = baseForward;
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Direction.RotationTowardsCorrectDriveDirection(transform, guidingDirction);
        targetForward = targetRotation * baseForward;
        // Debug.DrawLine(transform.position,transform.position + targetForward, Color.cyan);
        Vector3 topdownDir = new Vector3(targetForward.x, 0, targetForward.z);
        Vector3 topdownCarDir = new Vector3(transform.forward.x, 0, transform.forward.z);

        float projToCorrectDirction = Vector3.Dot(topdownDir, topdownCarDir);

        // print(projToCorrectDirction);

        float leftRightDeviation =
            Vector3.Dot(TorusTrack.Instance.transform.up, Vector3.Cross(topdownDir, topdownCarDir));
        if (leftRightDeviation > 0)
        {
            // print("right");
        }
        else if (leftRightDeviation < 0)
        {
            // print("left");
        }

        if (projToCorrectDirction >= 0)
        {
            aiInputValue[1] = 1;
        }

        else
        {
            aiInputValue[1] = -1;
        }

        aiInputValue[0] = -leftRightDeviation;
    }

    public Vector2 GetAIInput()
    {
        // print(aiInputValue);
        return aiInputValue;
    }
}