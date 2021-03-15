using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeapons_Manager : MonoBehaviour
{
    
    public string weaponActif = "Repulseur";
    /// <summary>
    /// c'est le sound ici , provisoire
    public string fmodSoundSpray;
    private FMOD.Studio.EventInstance event_fmod;
    /// </summary>
  

    //repulse
    public GameObject repulseWeapon;
    private bool _canRepulse;

    void Start ()
    {
        event_fmod = FMODUnity.RuntimeManager.CreateInstance(fmodSoundSpray);
    }

    

    private void Update()
    {
        if (Input.GetMouseButton(0) && weaponActif == "Repulseur")
        {
            _canRepulse = true;
            repulseWeapon.SetActive(true);
            
            
            /// ici c'est pour le sons: provisoire //
            ///
            event_fmod.start();
            ///
            /// 
        }
        else
        {
            /// ici c'est pour le sons: provisoire //
            ///
            event_fmod.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); 
            ///
            /// 
            _canRepulse = false;
            repulseWeapon.SetActive(false);
            
        }
        
    }
}
