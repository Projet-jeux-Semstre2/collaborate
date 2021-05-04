using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entities_Manager : MonoBehaviour
{
    [Header("Attraction")]
    public float pullRadius = 2;
    public float pullForce;

    public float pullForceMin;
    public float pullForceMax;
    [Space(25)]
    
    [Tooltip("Materiaux changeant")]
    public Material[] ChangeMaterials;
    private Rigidbody _rb;

    [Header("Rapport avec la formation d'un core")]
    public bool hasCore;
    public bool isInGroups;
    [Tooltip("Le core auquel j'appartiens")]
    public GameObject core;
    private GameObject _instCore;
    public GameObject myCore;
    [Tooltip("Nb minimum de particule pour former un core (sans compter elle même(si 5 alors faut 6)")]
    public int minCoreParticle = 2;
    [Space(2)]
    public List<GameObject> inMyGroup;

    public SphereCollider colliderAttirance;
    
    private GameObject _nearObj;

    public GameObject[] EntitiesType;
    







    private void OnEnable()
    {
        pullRadius = Random.Range(1, 7);
        pullForce = Random.Range(pullForceMin, pullForceMax);
        GetComponent<MeshRenderer>().material = ChangeMaterials[0];
        _rb = GetComponent<Rigidbody>();

        ObjectifExtermination.nrbEntités.Add(gameObject);

        float rdscale = Random.Range(transform.localScale.x - 1f, transform.localScale.x + 1f);
        
        transform.localScale = new Vector3(rdscale,rdscale,rdscale);

    }
    


    public void FixedUpdate()
    {
        
        
        
        

        if (!hasCore) //attirance des entités vers sois meme si je ne fais pas parti d'un groupe
        {
            /*_allCore.AddRange(GameObject.FindGameObjectsWithTag("core"));

            if (_allCore.Count > 0)
            {
                _nearObj = _allCore[0];
                for (int i = 0; i < _allCore.Count; i++)
                {
                    if (_allCore[i])
                    {
                        float distance = Vector3.Distance(_allCore[i].transform.position, transform.position);
                        if (distance < Vector3.Distance(_nearObj.transform.position, transform.position))
                        {
                            _nearObj = _allCore[i];
                        }
                    }
                    
                }
            }
            

            if (_nearObj)
            {
                transform.Translate(_nearObj.transform.position, Space.Self);
            }
            
            for (int i = _allCore.Count - 1; i > -1 ; i--)
            {
                if (_allCore[i] == null)
                {
                    _allCore.RemoveAt(i);
                }
            }*/
            
            
            colliderAttirance.enabled = true;

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
        else
        {
            colliderAttirance.enabled = false;
        }
        


        if (inMyGroup.Count >= minCoreParticle -1 && !hasCore && !_instCore) //toujours laisser -1 pour décompter le possèsseur
        {
            _instCore = Instantiate(core, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.identity);
        }

        


        if (!isInGroups)
        {
            inMyGroup.Clear();
        }
        

        if (!myCore)
        {
            hasCore = false;
        }
        
    }

    private void OnDrawGizmosSelected()
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && !hasCore)
        {
            isInGroups = false;
            
        }
    }

    private void OnDestroy()
    {
        if (myCore)
        {
            myCore.GetComponent<Core_Stats>().SuppStats(gameObject);
        }
        
        ObjectifExtermination.nrbEntités.Remove(gameObject);
    }
}
