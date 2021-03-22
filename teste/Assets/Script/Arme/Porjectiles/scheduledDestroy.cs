using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scheduledDestroy : MonoBehaviour
{
    public float time = 2.0f;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,time);
        
    }
    
}
