using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public float adjustSpeed;

    public float gravityCoeff;
    
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        rb.velocity = (y * speed * transform.forward + x  * speed * transform.right);
        
        int layerMask = LayerMask.GetMask("Wall");
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, Mathf.Infinity, layerMask))
        {
            transform.up = Vector3.Slerp(transform.up, hit.normal, adjustSpeed * Time.fixedDeltaTime) ;
            rb.AddForce( -1 * gravityCoeff * hit.normal);
        }
    }
}
