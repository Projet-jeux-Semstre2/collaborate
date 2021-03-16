using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeBasics : Weapon
{
   public GameObject projectilPrefab; // le types de bullet
   public float fireRate = 0.5f;
   public float launchImpulse = 10.0f;

   private float nextFireTime; // timer pour le fire Rate 

   public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
   {
      if (Time.time >= nextFireTime)
      {
         nextFireTime = Time.time + fireRate;
         fire(); // on tire selon le fire rate.
      }
   }
   private void fire()  // ici c'est la méthode ou l'on modifie la facon dont tire l'arme.
   {
      GameObject laserGo =  Instantiate(projectilPrefab, launchPoint.position, launchPoint.rotation);
      laserGo.GetComponent<Rigidbody>().AddForce(launchPoint.forward * launchImpulse,ForceMode.Impulse);
   }
}
