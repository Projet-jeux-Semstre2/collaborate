using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPompe : Weapon
{
    public GameObject projectilPrefab; // le types de bullet
    public float fireRate = 0.5f;
    public float maxRange = 10.0f;

    private RaycastHit hit;
    private float nextFireTime; // timer pour le fire Rate 
    
    
    
    
    public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            fire(); // on tire selon le fire rate.
        }
    }

    private void fire()
    {
        if (Physics.Raycast(launchPoint.transform.position, launchPoint.transform.forward, out hit, maxRange))
        {
            Instantiate(projectilPrefab, hit.point, Quaternion.identity);
        }
    }
}
