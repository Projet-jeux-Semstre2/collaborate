using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButtonRotation : MonoBehaviour
{
    public float rotationSpeedMax, rotationSpeedMin, rotationSpeed;

    private void OnEnable()
    {
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        float rd = Random.Range(0, 50);
        if (rd >= 25)
        {
            rotationSpeed *= 1;
        }
        else
        {
            rotationSpeed *= -1;
        }
        
    }

    private void Update()
    {
        transform.Rotate(0,0,rotationSpeed * Time.unscaledDeltaTime, Space.Self);
    }
}
