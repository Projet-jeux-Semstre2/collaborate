using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RepulseWeapon : MonoBehaviour
{
    public List<GameObject> objectsInRepulse;
    public GameObject player;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ennemis") && !objectsInRepulse.Contains(other.gameObject) ||other.gameObject.CompareTag("collider")&& !objectsInRepulse.Contains(other.gameObject))
        {
            objectsInRepulse.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ennemis") && objectsInRepulse.Contains(other.gameObject)||other.gameObject.CompareTag("collider")&& objectsInRepulse.Contains(other.gameObject))
        {
            if (other.GetComponentInParent<NavMeshAgent>() && other.gameObject.CompareTag("collider"))
            {
                other.GetComponentInParent<NavMeshAgent>().enabled = false;
            }
            objectsInRepulse.Remove(other.gameObject);
            
            
        }
    }

    private void Update()
    {
        if(objectsInRepulse.Count > 0)
        {
            foreach (GameObject cube in objectsInRepulse)
            {
                if (cube.tag == "ennemis")
                {
                    Rigidbody rb = cube.GetComponent<Rigidbody>();
                    

                    rb.AddForce(player.transform.forward * force, ForceMode.Impulse);
                }

                if (cube.tag == "collider")
                {
                    Rigidbody rb = cube.GetComponentInParent<Rigidbody>();

                    if (cube.GetComponentInParent<NavMeshAgent>())
                    {
                        cube.GetComponentInParent<NavMeshAgent>().enabled = false;
                    }

                    rb.AddForce(player.transform.forward * force, ForceMode.Impulse);
                }
                
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


    private void OnDisable()
    {
        foreach (var i in objectsInRepulse)
        {
            if (i.GetComponentInParent<NavMeshAgent>() && i.CompareTag("collider"))
            {
                i.GetComponentInParent<NavMeshAgent>().enabled = true;
            }
        }
        objectsInRepulse.Clear();
        
    }
}

