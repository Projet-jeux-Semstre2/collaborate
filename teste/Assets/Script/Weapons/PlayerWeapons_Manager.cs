using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons_Manager : MonoBehaviour
{
    
    public string weaponActif = "Repulseur";

    //repulse
    public GameObject repulseWeapon;
    private bool _canRepulse;


    

    private void Update()
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
        
    }
}
