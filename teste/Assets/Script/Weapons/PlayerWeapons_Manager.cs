using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons_Manager : Weapon
{

    
    //repulse
    public GameObject repulseWeapon;
    private bool _auto;

    public override void Engage()
    {
        Fire();
    }

    private void Fire()
    {
        _auto = true;
        repulseWeapon.SetActive(true);
    }

    public override void DisEngage()
    {
        _auto = false;
        repulseWeapon.SetActive(false);
    }
    

    
}
