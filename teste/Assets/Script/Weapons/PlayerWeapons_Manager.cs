using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeapons_Manager : Weapon
{

    public string weaponActif = "Repulseur";


    //repulse
    public GameObject repulseWeapon;
    private bool _canRepulse;

    public override void Engage()
    {
        Fire();
    }

    private void Fire()
    {
        _canRepulse = true;
        repulseWeapon.SetActive(true);
    }

    public override void DisEngage()
    {
        _canRepulse = false;
        repulseWeapon.SetActive(false);
    }
    


    /*private void Update()
{
    if (Input.GetMouseButton(0) && weaponActif == "Repulseur")
    {
        _canRepulse = true;
        repulseWeapon.SetActive(true);
    }
    else
    {
        _canRepulse = false;
        repulseWeapon.SetActive(false);
    }
    
}*/
}
