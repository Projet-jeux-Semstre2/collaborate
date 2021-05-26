using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSoundCore : MonoBehaviour
{
    private Core_Manager _coreManager;
    
    private GameObject _player;
    private float distance;
    public float maxDistance;
    
    // sons
    private FMOD.Studio.EventInstance event_fmod;
    public string FmodHeapMovement;
    private bool canPlayOnce;
    
    
    void Start()
    {
        // sons
        event_fmod = FMODUnity.RuntimeManager.CreateInstance( FmodHeapMovement);
        canPlayOnce = true;
        
        
        _coreManager = GetComponent<Core_Manager>();
        _player = _coreManager.player;
    }
    
    void Update()
    {
        event_fmod.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance< maxDistance)
        {
            if (canPlayOnce)
            {
                event_fmod.start();
                canPlayOnce = false;
            }
            
        }
        else
        {
            if (!canPlayOnce)
            {
                event_fmod.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //coupe les sons
            }
        }
    }
}