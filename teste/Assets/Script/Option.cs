using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Option : MonoBehaviour
{
    public StudioListener listener;
    public string fmodMuteSystem;
    private FMOD.Studio.EventInstance event_fmod;
    private bool mute;
    
    
    void Start ()
    {
        //event_fmod = FMODUnity.RuntimeManager.CreateInstance(fmodMuteSystem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !mute)
        {
            event_fmod.start();
            listener.enabled = false;
            mute = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && mute)
        {
            event_fmod.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            listener.enabled = true;
            mute = false;
        }
    }
}
