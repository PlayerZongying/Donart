using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float forceMagnitute = 1000;
    
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb.CompareTag("Player"))
        {
            Accelerate(rb);
        }
    }

    private void Accelerate(Rigidbody rb)
    {
        Vector3 forceDir = rb.velocity.magnitude != 0 ? rb.velocity.normalized : rb.transform.forward;
        Vector3 force = forceDir * forceMagnitute;
        rb.AddForce(force,ForceMode.Impulse);
    }
}
