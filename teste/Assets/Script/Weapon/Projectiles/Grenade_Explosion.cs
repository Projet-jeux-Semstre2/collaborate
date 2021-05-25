using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Explosion : MonoBehaviour
{
    public float damage;

    public float radius;
    public float exlosionRadius;

    public float force;
    public float upwardModifier;

    public ParticleSystem explosionParticle;
    public float timeBeforeExplose = 3;
    public LayerMask layerMaskExplosion;

    public string typeArmeActive;
    
    // sons
    public string fmodExploG;
    public Material[] emissivMaterial;
    public MeshRenderer emissivGrenade;

   

    private void Start()
    {
        switch (typeArmeActive)
        {
            case "ArmeForAgressif":
                emissivGrenade.material = emissivMaterial[3];
                break;
                    
            case "ArmeForTank":
                emissivGrenade.material = emissivMaterial[0];
                break;
                    
            case "ArmeForCreateur":
                emissivGrenade.material = emissivMaterial[1];
                break;
                    
            case "ArmeForVif":
                emissivGrenade.material = emissivMaterial[2];
                break;
        }
        
        StartCoroutine(ExplosionWithTime());
    }

    IEnumerator ExplosionWithTime()
    {
        yield return new WaitForSeconds(timeBeforeExplose);
        StartCoroutine(Explose());
    }
    


    IEnumerator Explose()
    {
        // sons 
        FMODUnity.RuntimeManager.PlayOneShot(fmodExploG, GetComponent<Transform>().position);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, exlosionRadius, layerMaskExplosion);
        foreach (Collider collide in colliders)
        {
            if (collide.gameObject.CompareTag("ennemis"))
            {
                Rigidbody rb = collide.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, exlosionRadius, upwardModifier, ForceMode.Impulse);
                }
            }
        }
        
        Collider[] destroys = Physics.OverlapSphere(transform.position, radius, layerMaskExplosion);
        foreach (Collider destroyed in destroys)
        {
            if (destroyed.gameObject.CompareTag("ennemis"))
            {
                destroyed.GetComponent<Entities_Stats>().TakeDamage(damage);
                
                switch (typeArmeActive)
                {
                    case "ArmeForAgressif":
                        if (destroyed.GetComponent<Entities_Manager>().type == "Agressif")
                        {
                            destroyed.GetComponent<Entities_Stats>().TakeDamage(damage *1000); //OneShot
                        }
                        break;
                    
                    case "ArmeForTank":
                        if (destroyed.GetComponent<Entities_Manager>().type == "Tank")
                        {
                            destroyed.GetComponent<Entities_Stats>().TakeDamage(damage *1000);
                        }
                        break;
                    
                    case "ArmeForCreateur":
                        if (destroyed.GetComponent<Entities_Manager>().type == "Createur")
                        {
                            destroyed.GetComponent<Entities_Stats>().TakeDamage(damage *1000);
                        }
                        break;
                    
                    case "ArmeForVif":
                        if (destroyed.GetComponent<Entities_Manager>().type == "Vif")
                        {
                            destroyed.GetComponent<Entities_Stats>().TakeDamage(damage * 1000);
                        }
                        break;
                }
                
            }
        }

        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
        
        
        
        yield return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ennemis"))
        {
            StartCoroutine(Explose());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exlosionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
    
}
