using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public Player player;
    public Camera mainCamera;
    
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public LayerMask touchingLayerMask;

    public GameObject[] viseur;
    
    

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        AfterEnable();
    }

    public virtual void AfterEnable()
    {
        
    }


    public  virtual void Engage()
    {
        
    }

    public virtual void DisEngage()
    {
        
    }
}
