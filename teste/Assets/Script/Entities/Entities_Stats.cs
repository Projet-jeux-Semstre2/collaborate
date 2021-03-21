using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities_Stats : MonoBehaviour
{
    
    [Header("Stats use")]
    public float health; 
    public float damage; 
    public float speed;
    
    
    public string FmodHurt;// sons
    
    public void TakeDamage(float amount)
    {
        // sons
        FMODUnity.RuntimeManager.PlayOneShot(FmodHurt, GetComponent<Transform>().position);
     
      
        health -= amount;
    }

    private void Update()
    {
        if ((health <= 0f))
        {
            Die();
        }
    }

    private void Die()
    {
        ObjectifExtermination.nrbTotal--;
        Destroy(gameObject);
    }
}
