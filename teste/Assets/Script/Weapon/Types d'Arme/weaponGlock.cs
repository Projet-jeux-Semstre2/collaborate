using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGlock : Weapon
{
    
    
    
    [Header("Stats de l'arme")]
    public float fireRate = 0.5f;
    public float maxRange = 10.0f;
    public float Degats = 1.0f;
    public float penetrationNumber;
    public float radiusExplosion;
    
    [Space(50)]
    public string fmodShoot; // sons

    private Glock_Manager _glockManager;
    
    private RaycastHit hit;

    
    
    private float nextFireTime; // timer pour le fire Rate 

    private void OnEnable()
    {
        foreach (var reticule in viseur)
        {
            reticule.SetActive(false);
        }
        viseur[0].SetActive(true);
        
        
    }

    private void Start()
    {
        _glockManager = GetComponentInParent<Glock_Manager>();
    }

    public override void Engage() // c'est override pour pouvoir réecrire la méthode du script dont elle hérite.
    {
        if (Time.time >= nextFireTime && !_glockManager.canRafal)
        {
            nextFireTime = Time.time + fireRate;
            fire(); // on tire selon le fire rate.
        }
        
        if (Time.time >= nextFireTime && _glockManager.canRafal)  //tire en rafale de 3
        {
            nextFireTime = Time.time + fireRate;
            StartCoroutine(fireRafale(_glockManager.rafaleLength, _glockManager.timeBetweenShoot)); // on tire selon le fire rate.
        }
    }

    public void fire()
    {
        // sons & FX 
        FMODUnity.RuntimeManager.PlayOneShot(fmodShoot);
        muzzleFlash.Play();
        
        if (viseurCanGrow)
        {
            StartCoroutine(ViseurFB());
        }
        
        
        /*if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxRange, touchingLayerMask))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Entities_Stats target = hit.transform.GetComponent<Entities_Stats>();
            if (target != null)
            {
                target.TakeDamage(Degats);
            }
        }*/
        List<RaycastHit> Hits = new List<RaycastHit>();
        
        Hits.AddRange(Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, maxRange, touchingLayerMask));

        float penetration = penetrationNumber;
        
        for (int i = 0; i < Hits.Count; i++)
        {
            if (Hits.Contains(Hits[i]) && penetration > 0)
            {
                print("ici");
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                Entities_Stats target = Hits[i].transform.GetComponent<Entities_Stats>();
                print(target);
                if (target != null)
                {
                    print("degat");
                    target.TakeDamage(Degats);
                    penetration--;
                }
                
            }
            
        }
        
        
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxRange, hitMarkerLayer))
        {
            StartCoroutine(HitMarker());
        }
    }


    IEnumerator fireRafale(float rafaleLenght, float timeBetweenShoot)
    {
        for (int i = 0; i < rafaleLenght; i++)
        {
            fire();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
}
