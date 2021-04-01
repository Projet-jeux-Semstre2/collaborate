using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMitraillette : Weapon
{
    public GameObject projectilPrefab; // le types de bullet
    public float fireRate = 0.5f;
    public float launchImpulse = 10.0f;
    private float nextFireTime;

    private bool _auto;

    public override void Engage()
    {
        _auto = true;
    }

    public override void DisEngage()
    {
        _auto = false;
    }

    private void Update()
    {
        if (_auto)
        {
            fire();
        }
    }
    
    private void fire()  // ici c'est la méthode ou l'on modifie la facon dont tire l'arme.
    {
        GameObject laserGo =  Instantiate(projectilPrefab, launchPoint.position, launchPoint.rotation);
        laserGo.GetComponent<Rigidbody>().AddForce(launchPoint.forward * launchImpulse,ForceMode.Impulse);
    }
}
