using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public Camera mainCamera;
    
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public LayerMask touchingLayerMask;

    public GameObject[] viseur;


    public  virtual void Engage()
    {
        
    }

    public virtual void DisEngage()
    {
        
    }
}
