using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtEntitties : MonoBehaviour
{
   public string FmodHurt;// sons
   
   private Entities_Manager _entitiesManager;
   private float health;

   private void Start()
   {
      _entitiesManager = GetComponent<Entities_Manager>();
      health = _entitiesManager.health;
   }

   public void TakeDamage(float amount)
   {
      // sons
      FMODUnity.RuntimeManager.PlayOneShot(FmodHurt, GetComponent<Transform>().position);
     
      
      health -= amount;
      if ((health <= 0f))
      {
         Die();
      }
   }

   private void Die()
   {
      Destroy(gameObject);
   }

}

