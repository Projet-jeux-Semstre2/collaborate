using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scheduledDestroy : MonoBehaviour
{
    public float time = 2.0f;
    public bool destroy = true;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (destroy)
        {
            Destroy(gameObject,time);
        }
        else
        {
            StartCoroutine(Disable());
        }



    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    
}
