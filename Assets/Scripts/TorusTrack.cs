using UnityEngine;
using Random = UnityEngine.Random;

public struct PositionOnTorus
{
    public float thetaInDegree;
    public float phiInDegree;
}

public class TorusTrack : MonoBehaviour
{
    const float R_Model = 3f;
    const float r_Model = 2f;
    const float BaseScale = 4f;
    public static TorusTrack Instance;


    [SerializeField] private float scale = 4f;
    public static float ScaleForChildren{ get; private set; }
    public static float R { get; private set; }
    public static float r { get; private set; }

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

        transform.localScale = scale * Vector3.one;
        R = R_Model * scale;
        r = r_Model * scale;
    }


    public static Vector3 PositionOnTorusSurface(float thetaInDegree, float phiInDegree)
    {
        Vector3 pos = Vector3.one;

        float thetaInRadius = thetaInDegree / 360 * 2 * Mathf.PI;

        pos = new Vector3(Mathf.Cos(thetaInRadius), Mathf.Sin(thetaInRadius), 0) * r + Vector3.right * R;
        pos = Quaternion.Euler(0, phiInDegree, 0) * pos;
        pos = Instance.transform.rotation * pos;
        pos += Instance.transform.position;
        return pos;
    }

    public static Vector3 RandomPositionOnTorusSurface()
    {
        float randomThetaInDegree = Random.Range(0f, 360f);
        float randomPhiInDegree = Random.Range(0f, 360f);
        return PositionOnTorusSurface(randomThetaInDegree, randomPhiInDegree);
    }

    public static Quaternion OrientaionOnTorusSurface(float thetaInDegree, float phiInDegree)
    {
        float thetaInRadius = thetaInDegree / 360f * 2 * Mathf.PI;
        float phiInRadius = phiInDegree / 360f * 2 * Mathf.PI;
        Vector3 forward = new Vector3(Mathf.Sin(phiInRadius), 0, Mathf.Cos(phiInRadius));
        forward = Instance.transform.rotation * forward;

        Vector3 up = -new Vector3(Mathf.Cos(thetaInRadius), Mathf.Sin(thetaInRadius), 0);
        up = Quaternion.Euler(0, phiInDegree, 0) * up;
        up = Instance.transform.rotation * up;

        Quaternion Orientation = Quaternion.LookRotation(forward, up);
        return Orientation;
    }

    private void Update()
    {
        transform.localScale = scale * Vector3.one;
        R = R_Model * scale;
        r = r_Model * scale;
    }
}