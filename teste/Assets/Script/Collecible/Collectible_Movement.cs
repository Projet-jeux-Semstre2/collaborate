using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible_Movement : MonoBehaviour
{
    public Vector3 currentSpeedRotation;
    public Vector3 speedRotationMin;
    public Vector3 speedRotationMax;

    private void Start()
    {
        currentSpeedRotation.x = Random.Range(speedRotationMin.x, speedRotationMax.x);
        currentSpeedRotation.y = Random.Range(speedRotationMin.y, speedRotationMax.y);
        currentSpeedRotation.z = Random.Range(speedRotationMin.z, speedRotationMax.z);
    }

    private void Update()
    {
        transform.Rotate(currentSpeedRotation.x * Time.deltaTime,currentSpeedRotation.y * Time.deltaTime,currentSpeedRotation.z * Time.deltaTime, Space.Self);
    }
}
