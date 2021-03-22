using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_renderer : MonoBehaviour
{
    public Transform core;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;


    private void Update()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(core.position.x, core.position.y + 0.5f,core.position.z), ref velocity, smoothTime);
    }
}
