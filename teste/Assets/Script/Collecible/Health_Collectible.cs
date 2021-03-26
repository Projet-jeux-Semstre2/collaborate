using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Collectible : MonoBehaviour
{
    public float healthGive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Player_Health>().health < 100)
            {
                other.GetComponent<Player_Health>().health += healthGive;
                Destroy(gameObject);
            }
            
        }
    }
}
