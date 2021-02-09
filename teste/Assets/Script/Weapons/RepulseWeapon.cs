using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulseWeapon : MonoBehaviour
{
    public List<GameObject> objectsInRepulse;
    public GameObject player;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ennemis"))
        {
            objectsInRepulse.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ennemis"))
        {
            objectsInRepulse.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if(objectsInRepulse.Count > 0)
        {
            foreach (GameObject cube in objectsInRepulse)
            {
                Rigidbody rb = cube.GetComponent<Rigidbody>();

                rb.AddForce(player.transform.forward * force, ForceMode.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            force = 1.25f;
        }
        if (Input.GetKey(KeyCode.X))
        {
            force = 1.75f;
        }

    }
}

