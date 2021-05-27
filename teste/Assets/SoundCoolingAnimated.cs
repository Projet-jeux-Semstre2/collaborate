using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCoolingAnimated : MonoBehaviour
{

    private FMOD.Studio.EventInstance event_fmod;
    public string FmodCoolingBoucle;
    void Start ()
    {
        event_fmod = FMODUnity.RuntimeManager.CreateInstance(FmodCoolingBoucle);
    }

    void PlaySound(string var)
    {
        event_fmod.start();
    }

    void StopSound(string var)
    {
        event_fmod.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    void PlaysSoundOS()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FmodCoolingBoucle, GetComponent<Transform>().position);
    }
}
