using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class weaponPompe : Weapon
{
    public float fireRate = 0.5f;
    public float maxRange = 10.0f;
    public float Degats = 1.0f;
    public string fmodShoot; // sons
    public int vitessWalk = 3;
    public int vitesseSrint = 5;
    public Vector3 Combo;

    private RaycastHit hit;
    private float nextFireTime; // timer pour le fire Rate 


    public int tromblonNombre = 10;
    private List<Quaternion> tromblonsDirection;
    public float sprayX = 0.2f;

    private Animator _animator;

    private Surchauffe_Manager _surchauffeManager;
    




    public override void AfterEnable()
    {
        _surchauffeManager = GetComponentInParent<Surchauffe_Manager>();
        _animator = GetComponent<Animator>();
        

        player.walkingSpeed -= vitessWalk;
        player.runningSpeed -= vitesseSrint;
        
        foreach (var reticule in viseur)
        {
            reticule.SetActive(false);
        }
        viseur[0].SetActive(true);

    }
    
    

    private void OnDisable()
    {
        player.runningSpeed = player.initRunSpeed;
        player.walkingSpeed = player.initWalkSpeed;
    }

    public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
    {
        if (_surchauffeManager.niveauSurchauffe < _surchauffeManager.surchauffe)
        {
            tromblonsDirection = new List<Quaternion>(tromblonNombre);
            for (int i = 0; i < tromblonNombre; i++)
            {
                tromblonsDirection.Add(Quaternion.Euler(Vector3.zero));
            }
       
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                fire(); // on tire selon le fire rate.
            }
        }
        
    }
    private void RayTromblonsShot(Vector3 var)
    {
        if (Physics.Raycast(mainCamera.transform.position, var, out hit, maxRange, touchingLayerMask))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Entities_Stats target = hit.transform.GetComponent<Entities_Stats>();
            if (target != null)
            {
                target.TakeDamage(Degats);
            }
        }
    }

    private void fire()
    {
       
        
        // sons & FX 
        _animator.SetTrigger("Shoot");
        FMODUnity.RuntimeManager.PlayOneShot(fmodShoot);
        muzzleFlash.Play();
        
        _surchauffeManager.niveauSurchauffe += 1;
        
        int i = 0;
        foreach (var VARIABLE in tromblonsDirection)
        {
            Combo = mainCamera.transform.position + mainCamera.transform.forward * 1000.0f;
            // Bloom
            Combo += Random.Range(-sprayX,sprayX) * mainCamera.transform.up;
            Combo += Random.Range(-sprayX,sprayX) * mainCamera.transform.right;
            Combo -= mainCamera.transform.position;
            Combo.Normalize();
            RayTromblonsShot(Combo);
            i++;
        }
    }

    private void Update()
    {
        Debug.DrawLine(mainCamera.transform.position, new Vector3(0, 0, maxRange) + new Vector3(Combo.x, Combo.y, 0));
    }
}
