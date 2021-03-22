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
    
    
    public string FmodHurt;
    public ParticleSystem dieExplosion;

    public Material hitMaterial;
    public Material fullLifeMaterial;

    private MeshRenderer _meshRenderer;

    private float t;

    private bool _canBeDamaged = true;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void TakeDamage(float amount)
    {

        FMODUnity.RuntimeManager.PlayOneShot(FmodHurt, GetComponent<Transform>().position);
     
        health -= amount;
        if ((health <= 0f))
        {
            Die();
        }

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

    void Die()
    {
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
