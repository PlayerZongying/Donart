using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public float adjustSpeed;

    public float gravityCoeff;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
            // Debug.Log("Did Hit");
            // Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
            // Debug.Log(hit.point);

            transform.up = Vector3.Slerp(transform.up, hit.normal, adjustSpeed * Time.fixedDeltaTime) ;
            rb.AddForce( -1 * gravityCoeff * hit.normal);
        }
        else
        {
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * 1000, Color.white);
            // Debug.Log("Did not Hit");
        }
    }
}
