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
    public float surchauffeAdd;

    [Header("Reload")]
    [Tooltip("Vitesse multiplicateur de la reload")]
    public float reloadSpeed;
    public bool Reloading;

    [Header("Balles explosives")]
    public float explosionForce;
    public float explosionRadius;
    
    [Header("Rocket Jump")]
    public GameObject bulletExplosePlayer;
    public float bulletExplosionForce;
    
    [Space (50)]
    // sons
    public string fmodShoot; 
    public string fmodLaunchGrenade;
    public string FmodCooling;
    private FMOD.Studio.EventInstance event_fmod_Cooling;
     private bool soundOs = true;
     
    
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
    public float surchauffeAddLanceGrenade;


    public ShakeFeedback shakeFeedback;
    




    public override void AfterEnable()
    {
        _shotgunManager = GetComponentInParent<ShotGun_Manager>();
        _animator = GetComponent<Animator>();
        _explosion = impactEffect.GetComponent<ExplosiveDestroy>();
        

        player.walkingSpeed -= vitessWalk;

        foreach (var reticule in viseur)
        {
            reticule.SetActive(false);
        }
        viseur[0].SetActive(true);

    }
    
    void Start ()
    {
        event_fmod_Cooling = FMODUnity.RuntimeManager.CreateInstance(FmodCooling);
    }

    

    private void OnDisable()
    {
        player.walkingSpeed = player.initWalkSpeed;
    }

    public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
    {
        if (_shotgunManager.niveauSurchauffe < _shotgunManager.surchauffe && !Reloading && !_shotgunManager.isSurchauffeMax)
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
            GameObject bullet = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            bullet.GetComponent<ExplosiveDestroy>().force = explosionForce;
            bullet.GetComponent<ExplosiveDestroy>().radius = explosionRadius;
            
            if (_shotgunManager.canExplosePlayer)
            {
               GameObject explo= Instantiate(bulletExplosePlayer, hit.point, Quaternion.identity);
               explo.GetComponent<ExplosionJumpPlayer>().force = bulletExplosionForce;
            }
            
            
            Entities_Stats target = hit.transform.GetComponent<Entities_Stats>();
            if (target != null)
            {
                target.TakeDamage(Degats);
                switch (_shotgunManager.typeArmeActive)
                {
                    case "ArmeForAgressif":
                        if (target.GetComponent<Entities_Manager>().type == "Agressif")
                        {
                            target.TakeDamage(Degats *Mathf.Infinity); //OneShot
                        }
                        break;
                    
                    case "ArmeForTank":
                        if (target.GetComponent<Entities_Manager>().type == "Tank")
                        {
                            target.TakeDamage(Degats *Mathf.Infinity );
                        }
                        break;
                    
                    case "ArmeForCreateur":
                        if (target.GetComponent<Entities_Manager>().type == "Createur")
                        {
                            target.TakeDamage(Degats *Mathf.Infinity);
                        }
                        break;
                    
                    case "ArmeForVif":
                        if (target.GetComponent<Entities_Manager>().type == "Vif")
                        {
                            target.TakeDamage(Degats *Mathf.Infinity);
                        }
                        break;
                }
                
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
        if (!_shotgunManager.shotGunAnimator.GetBool("OverLoadPlaying"))
        {
            _animator.SetTrigger("Shoot");
        }
        
        shakeFeedback.PlayShake();
        
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

       /* if (Input.GetButtonDown("Reload")) /// son quand appyer déclenche
        {
            if (soundOs)
            {
                event_fmod_Cooling = FMODUnity.RuntimeManager.CreateInstance(FmodCooling);
                soundOs = false;
            }
        }*/
        
        if (Input.GetButton("Reload") && !_shotgunManager.isSurchauffeMax)
        {
            _shotgunManager.shotGunAnimator.SetBool("OverLoad", true);
            Reload();
        }
        else if(Reloading)
        {
            _shotgunManager.shotGunAnimator.SetBool("OverLoad", false);
            Reloading = false;
            
        }

        if (Input.GetButtonUp("Reload")) // quand laiser tomber reduit
        {
           
            FMODUnity.RuntimeManager.PlayOneShot(FmodCooling);
        }
    }
    
    public override void Engage2nd()
    {
        if ( !Reloading&& _shotgunManager.lanceGrenadeUnlock && _shotgunManager.niveauSurchauffe < _shotgunManager.surchauffe && !_shotgunManager.isSurchauffeMax)
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
        // sons
        FMODUnity.RuntimeManager.PlayOneShot(fmodLaunchGrenade);
        if (viseurCanGrow)
        {
            StartCoroutine(ViseurFB());
        }
        
        _shotgunManager.niveauSurchauffe += surchauffeAddLanceGrenade;

        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitGrenade, Mathf.Infinity, LayerMaskGrenade );
        _distance= Vector3.Distance(hitGrenade.point, mainCamera.transform.position);
        _yAddHit = 0.08295799f * _distance; //Permet d'avoir la balle qui touche le centre du cross air (si c'est possible avec la gravité)
        GameObject grenadeInst = Instantiate(grenade, launchPoint.position, Quaternion.identity);
        grenadeInst.transform.LookAt(new Vector3(hitGrenade.point.x , hitGrenade.point.y + _yAddHit, hitGrenade.point.z));
        grenadeInst.GetComponent<Grenade_Explosion>().typeArmeActive = _shotgunManager.typeArmeActive;
        grenadeInst.GetComponent<Rigidbody>().AddForce(grenadeInst.transform.forward * shootForce, ForceMode.Impulse);
    }

    void Reload()
    {
        if (_shotgunManager.niveauSurchauffe > 0 )
        {
            Reloading = true;
            _shotgunManager.niveauSurchauffe -= reloadSpeed * Time.deltaTime;

            
        }
        
        
        
    }
    
}
