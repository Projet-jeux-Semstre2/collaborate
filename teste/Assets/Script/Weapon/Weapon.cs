using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Principaux")]
    public Transform launchPoint;
    public Player player;
    public Camera mainCamera;
    
    [Header("Feedback")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject hitMarker;
    [Tooltip("Temps viseul du hitMarker")]
    public float hitMarkerTime;
    [Header("LayerMask")]
    public LayerMask touchingLayerMask;
    public LayerMask hitMarkerLayer;
    
    

    [Header("Viseur")]
    
    public float initialSize;
    public float maxSize;
    public bool viseurCanGrow= true;
    public GameObject[] viseur;
    public GameObject viseurActive;
    
    [Header("Temps Entre Changements D'Arme")]
    public float timeBetweenChange;

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

    public virtual void Engage2nd()
    {
        
    }

    public virtual void DisEngage()
    {
        
    }

    public virtual IEnumerator HitMarker()
    {
        hitMarker.SetActive(true);
        
        while (hitMarker.transform.localScale.x < .15f * 3f)
        {
            float lerp = Mathf.Lerp(hitMarker.transform.localScale.x, .15f * 4f, Time.deltaTime * 25);
            hitMarker.transform.localScale = new Vector3(lerp,lerp,lerp);
            yield return null;
        }
        
        
        yield return new WaitForSeconds(hitMarkerTime);
        
        hitMarker.transform.localScale = new Vector3(0.15f,.15f,.15f);
        hitMarker.SetActive(false);
    }

    public virtual IEnumerator ViseurFB()
    {
        viseurCanGrow = false;
        while (viseurActive.transform.localScale.x < maxSize)
        {
            float lerp = Mathf.Lerp(viseurActive.transform.localScale.x, maxSize*1.2f, Time.deltaTime * 10);
            viseurActive.transform.localScale = new Vector3(lerp,lerp,lerp);
            yield return null;
        }

        while (viseurActive.transform.localScale.x > initialSize)
        {
            float lerp = Mathf.Lerp(viseurActive.transform.localScale.x, initialSize/2, Time.deltaTime * 10);
            viseurActive.transform.localScale = new Vector3(lerp,lerp,lerp);
            yield return null;
        }
        
        viseurActive.transform.localScale = new Vector3(initialSize,initialSize,initialSize);
        viseurCanGrow = true;
    }
}
