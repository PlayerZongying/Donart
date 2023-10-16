using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusInMainMenu : MonoBehaviour
{
    [Header("Rotation Speed")] 
    public float xSpeed = 10;
    public float ySpeed = 10;
    public float zSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        // gimbo lock
        //transform.eulerAngles += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;

        transform.rotation =
            Quaternion.Euler(new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime) * transform.rotation;
    }
}