﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entities_Stats : MonoBehaviour
{
    private Entities_Manager _entitiesManager;
    [Header("Stats use")]
    public float health; 
    public float damage; 
    public float speed;
    
    [Header("Initial Stats")]
    public float InitHealth; 
    public float InitDamage; 
    public float InitSpeed;
    
    // SONS
    public string FmodHurt;
    public string FmodDie;
    public ParticleSystem dieExplosion;

    public Material hitMaterial;
    public Material fullLifeMaterial;

    public ShotGun_Manager shotGunManager;
    
    private Glock_Manager _glockManager;
    
    

    private MeshRenderer _meshRenderer;

    private float t;

    private bool _canBeDamaged = true;

    [Header("Horloge Interne")]
    public float horlogeInterne;
    public float horlogeTime;
    public float maxTime;
    public float minTime;
    public float heatlh_augmented;
    public float damage_augmented;
    public float speed_augmented;

    private float saveStatSpeed, saveStatDamage;

    public bool horlogeFinish;

    public GameObject overloadCollectible;
    

    private void Start()
    {
        _entitiesManager = GetComponent<Entities_Manager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        shotGunManager = GameObject.FindWithTag("Player").GetComponent<ShotGun_Manager>();
        

        horlogeInterne = Random.Range(minTime, maxTime);

        InitDamage = damage;
        InitHealth = health;
        InitSpeed = speed;

        

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
            _meshRenderer.material.SetColor("_EmissionColor", Color.Lerp(_meshRenderer.material.GetColor("_EmissionColor"), hitMaterial.GetColor("_EmissionColor"), t*2));
            _canBeDamaged = false;
            yield return null;
        }

        t = 0;

        while (t < .5f)
        {
            t += Time.deltaTime;
            _meshRenderer.material.SetColor("_EmissionColor", Color.Lerp(_meshRenderer.material.GetColor("_EmissionColor"), fullLifeMaterial.GetColor("_EmissionColor"), t));
            _canBeDamaged = true;
            yield return null;
        }
        
        t = 0;
        _canBeDamaged = true;
        yield return null;
    }

    private void Update()
    {
        if (health <= 0f || health == Single.NaN)
        {
            Die();
        }

        horlogeTime += Time.deltaTime;
        if (horlogeTime >= horlogeInterne || horlogeFinish)
        {
            HorlogeInterne();
        }

        
    }

    void Die()
    {
        // sons
        //FMODUnity.RuntimeManager.PlayOneShot(FmodDie, transform.position);
        
        Instantiate(dieExplosion, transform.position, Quaternion.identity);

        if(shotGunManager.typeArmeActive == "ArmeForTank" && _entitiesManager.type == "Tank")
        {
            shotGunManager.niveauSurchauffe -= .5f;
        }
        
        if(shotGunManager.typeArmeActive == "ArmeForCreateur" && _entitiesManager.type == "Createur")
        {
            shotGunManager.niveauSurchauffe -= .5f;
        }
        if(shotGunManager.typeArmeActive == "ArmeForVif" && _entitiesManager.type == "Vif")
        {
            shotGunManager.niveauSurchauffe -= .5f;
        }
        if(shotGunManager.typeArmeActive == "ArmeForAgressif" && _entitiesManager.type == "Agressif")
        {
            shotGunManager.niveauSurchauffe -= .5f;
        }
        
        
        Destroy(gameObject);
    }



    public void HorlogeInterne()
    {
        if (_entitiesManager.hasCore)
        {
            //Stockage anciennes stats
            saveStatDamage = damage;
            saveStatSpeed = speed;
            
            //Nouvelles stats
            health += heatlh_augmented;
            damage += damage_augmented;
            speed += speed_augmented;
            
            //Ajout au core
            _entitiesManager.myCore.GetComponent<Core_Manager>().agent.speed += speed - saveStatSpeed;
            _entitiesManager.myCore.GetComponent<Core_Attack>().cacDamage += damage - saveStatDamage;
            
            
        }
        
        if (!_entitiesManager.hasCore)
        {
            EntitiesCreate(5);
        }

        
        horlogeTime = 0;
        horlogeInterne = Random.Range(minTime, maxTime);
        horlogeFinish = false;
        
    }

    void EntitiesCreate(float spawnRadius)
    {
        bool hasCreate = false;
        if (!hasCreate)
        {
            Vector3 rdPosition = Random.insideUnitSphere * spawnRadius + transform.position;
            rdPosition.y = transform.position.y;
            int rdEntities = Random.Range(0, _entitiesManager.EntitiesType.Length);
            GameObject instantiate = Instantiate(_entitiesManager.EntitiesType[rdEntities], rdPosition, Quaternion.identity);
            hasCreate = true;
        }
    }

    
    
}
