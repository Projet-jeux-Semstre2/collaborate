using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entities_Manager : MonoBehaviour
{
    public float pullRadius = 2;
    public float pullForce;

    public float pullForceMin;
    public float pullForceMax;
    

    public Material[] ChangeMaterials;
    private Rigidbody _rb;

    public bool hasCore;
    public bool isInGroups;
    public GameObject core;
    private GameObject _instCore;
    public GameObject myCore;
    public List<GameObject> inMyGroup;
    public int minCoreParticle = 2;
    private void Start()
    {
        pullRadius = Random.Range(1, 7);
        pullForce = Random.Range(pullForceMin, pullForceMax);
        GetComponent<MeshRenderer>().material = ChangeMaterials[0];
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!hasCore) //attirance des entités vers sois meme si je ne fais pas parti d'un groupe
        {
            GetComponent<SphereCollider>().enabled = true;
            
            foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius) )
            {
                
                if (collider.gameObject.CompareTag("ennemis"))
                {
                    // calculate direction from target to me
                    Vector3 forceDirection = transform.position - collider.transform.position;

                    // apply force on target towards me
                    collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                }
            }
        }
        


        if (inMyGroup.Count > minCoreParticle -1 && !hasCore && _instCore == null) //toujours laisser -1 pour décompter le possèsseur
        {
            _instCore = Instantiate(core, transform.position, Quaternion.identity);
        }

        
        if (_rb.velocity == Vector3.zero)
        {
            pullForce = 0;
            GetComponent<MeshRenderer>().material = ChangeMaterials[1];
        }
        
        if (_rb.velocity != Vector3.zero)
        {
            pullForce = Random.Range(pullForceMin, pullForceMax);
            GetComponent<MeshRenderer>().material = ChangeMaterials[0];
        }


        if (!isInGroups)
        {
            inMyGroup.Clear();
        }

        if (myCore == null)
        {
            hasCore = false;
        }
        
    }

    
    
    private void OnDrawGizmos()
    {
        if (!hasCore || !isInGroups)
        {
            Gizmos.DrawWireSphere(transform.position, pullRadius);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ennemis") && !hasCore)
        {
            isInGroups = true;
            
            if (!inMyGroup.Contains(other.gameObject))
            {
                inMyGroup.Add(other.gameObject);
            }
            //other.GetComponent<Entities_Manager>().isInGroups = true;

            /*if (!other.GetComponent<Entities_Manager>().hasCore && other.GetComponent<Entities_Manager>().isInGroups && isInGroups)
            {
                _instCore = Instantiate(core, transform.position, Quaternion.identity);
                print("je créer un core");
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && !hasCore)
        {
            isInGroups = false;
            
        }
    }
}
