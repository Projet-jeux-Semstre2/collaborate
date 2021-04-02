using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities_Stats : MonoBehaviour
{
    
    [Header("Stats use")]
    public float health; 
    public float damage; 
    public float speed;
    
    // SONS
    public string FmodHurt;
    public string FmodDie;
    public ParticleSystem dieExplosion;

    public Material hitMaterial;
    public Material fullLifeMaterial;

    private ShotGun_Manager _shotGunManager;
    private Glock_Manager _glockManager;
    


    private MeshRenderer _meshRenderer;

    private float t;

    private bool _canBeDamaged = true;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _shotGunManager = GameObject.FindWithTag("Player").GetComponent<ShotGun_Manager>();
        _glockManager = GameObject.FindWithTag("Player").GetComponent<Glock_Manager>();
    }

    public void TakeDamage(float amount)
    {
        //sons
        FMODUnity.RuntimeManager.PlayOneShot(FmodHurt, GetComponent<Transform>().position);
     
        health -= amount;
        

        if (_canBeDamaged)
        {
            StartCoroutine(Feedback());
        }
        
    }

    IEnumerator Feedback()
    {
        while (t < 1)
        {
            t += Time.deltaTime;
            _meshRenderer.material.Lerp(_meshRenderer.material, hitMaterial, t * 2);
            _canBeDamaged = false;
            yield return null;
        }

        t = 0;

        while (t < .5f)
        {
            t += Time.deltaTime;
            _meshRenderer.material.Lerp(_meshRenderer.material, fullLifeMaterial, t);
            _canBeDamaged = true;
            yield return null;
        }
        
        t = 0;
        _canBeDamaged = true;
        yield return null;
    }

    private void Update()
    {
        if ((health <= 0f))
        {
            Die();
        }
    }

    void Die()
    {
        // sons
        //FMODUnity.RuntimeManager.PlayOneShot(FmodDie, transform.position);
        
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
        
        
        if (_shotGunManager.weaponPompe.gameObject.activeInHierarchy)
        {
            if (_shotGunManager.palierJauge < _shotGunManager.niveauBetweenPalier.Length)
            {
                _shotGunManager.niveauJauge++;
            }
            
        }
        
        if(_glockManager.weaponGlock.gameObject.activeInHierarchy)
        {
            if (_glockManager.palierJauge < _glockManager.niveauBetweenPalier.Length)
            {
                _glockManager.niveauJauge++;
            }
            
        }
        
        Destroy(gameObject);
    }
    
}
