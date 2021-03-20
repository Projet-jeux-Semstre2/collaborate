using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGlock : Weapon
{
    public GameObject projectilPrefab; // le types de bullet
    public float fireRate = 0.5f;
    public float maxRange = 10.0f;
    public float Degats = 1.0f;
    public string fmodShoot; // sons
    
    
    
    private RaycastHit hit;
    
    private float nextFireTime; // timer pour le fire Rate 

    private void OnEnable()
    {
        foreach (var reticule in viseur)
        {
            reticule.SetActive(false);
        }
        viseur[0].SetActive(true);
    }


    public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
    {
        if (Time.time >= nextFireTime)
        {
            Debug.Log("shootpompe");
            nextFireTime = Time.time + fireRate;
            fire(); // on tire selon le fire rate.
        }
    }

    public void fire()
    {
        // sons & FX 
        FMODUnity.RuntimeManager.PlayOneShot(fmodShoot);
        muzzleFlash.Play();
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxRange, touchingLayerMask))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            
            HealtEntitties target = hit.transform.GetComponent<HealtEntitties>();
            if (target != null)
            {
                target.TakeDamage(Degats);
            }
        }
    }
}
