using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusTrack : MonoBehaviour
{
    const float R_Model = 3f;
    const float r_Model = 2f;

    public static TorusTrack Instance;

    [SerializeField] private float scale = 4f;
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

    private void Start()
    {
    }
}