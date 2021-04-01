using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class weaponPompe : Weapon
{
    private ExplosiveDestroy _explosion;
    
    [Header("Stats de l'arme")]
    public float fireRate = 0.5f;
    public float maxRange = 10.0f;
    public float Degats = 1.0f;
    [Tooltip("nombre de balle par tir")]
    public int tromblonNombre = 10;
    [Tooltip("Taille du cone de tir")]
    public float sprayX = 0.2f;

    public float explosionForce;
    
    [Space (50)]
    public string fmodShoot; // sons
    
    [Header("Vitesse du joueur")]
    public int vitessWalk = 3;
    public int vitesseSrint = 5;
    
    [Space(35)]
    public Vector3 Combo;
    private RaycastHit hit;
    private float nextFireTime; // timer pour le fire Rate 


    private List<Quaternion> tromblonsDirection;
    
    private Animator _animator;
    private ShotGun_Manager _shotgunManager;

    [Header("Lance-Grenade")]
    public GameObject grenade;
    public float fireRateGrenade;
    private float _nextFireTimeGrenade;
    public ParticleSystem muzzleFlashgrenade;
    public float shootForce;
    public RaycastHit hitGrenade;
    public LayerMask LayerMaskGrenade;
    public float _yAddHit;
    private float _distance;
    public float surchauffeAdd;





    public override void AfterEnable()
    {
        
        _shotgunManager = GetComponentInParent<ShotGun_Manager>();
        _animator = GetComponent<Animator>();
        _explosion = impactEffect.GetComponent<ExplosiveDestroy>();
        

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
        if (_shotgunManager.niveauSurchauffe < _shotgunManager.surchauffe)
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

        if (Physics.Raycast(mainCamera.transform.position, var, out hit, maxRange, hitMarkerLayer))
        {
            StartCoroutine(HitMarker());
        }
    }

    private void fire()
    {
       
        // sons & FX 
        _animator.SetTrigger("Shoot");
        FMODUnity.RuntimeManager.PlayOneShot(fmodShoot);
        muzzleFlash.Play();

        if (viseurCanGrow)
        {
            StartCoroutine(ViseurFB());
        }
        
        
        _shotgunManager.niveauSurchauffe += 1;
        
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
        _explosion.force = explosionForce;
        Debug.DrawLine(mainCamera.transform.position, new Vector3(0, 0, maxRange) + new Vector3(Combo.x, Combo.y, 0));
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.blue, 0.5f);
    }
    
    public override void Engage2nd()
    {
        if (_shotgunManager.niveauSurchauffe < _shotgunManager.surchauffe)
        {
            if (Time.time >= _nextFireTimeGrenade)
            {
                _nextFireTimeGrenade = Time.time + fireRateGrenade;
                fireGrenade();
            }
        }
    }

    void fireGrenade()
    {
        muzzleFlash.Play();

        if (viseurCanGrow)
        {
            StartCoroutine(ViseurFB());
        }
        
        _shotgunManager.niveauSurchauffe += surchauffeAdd;

        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitGrenade, Mathf.Infinity, LayerMaskGrenade );
        _distance= Vector3.Distance(hitGrenade.point, mainCamera.transform.position);
        _yAddHit = 0.08295799f * _distance; //Permet d'avoir la balle qui touche le centre du cross air (si c'est possible avec la gravité)
        GameObject grenadeInst = Instantiate(grenade, launchPoint.position, Quaternion.identity);
        grenadeInst.transform.LookAt(new Vector3(hitGrenade.point.x , hitGrenade.point.y + _yAddHit, hitGrenade.point.z));
        grenadeInst.GetComponent<Rigidbody>().AddForce(grenadeInst.transform.forward * shootForce, ForceMode.Impulse);


    }
    
}
