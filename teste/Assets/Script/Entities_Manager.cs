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
    [Space(10)]
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
    
    
    
    
    public enum Forme {Cube, Cone, Triangle, Icosphere}
    [Header("Choix de la forme")]
    [Tooltip("Choisis la forme que tu veut pour cette entité")]
    public Forme forme;
    public Mesh cubeMesh;
    public Mesh coneMesh;
    public Mesh triangleMesh;
    public Mesh icosphereMesh;
    
    private Mesh _myMesh;
    private void Start()
    {
        pullRadius = Random.Range(1, 7);
        pullForce = Random.Range(pullForceMin, pullForceMax);
        GetComponent<MeshRenderer>().material = ChangeMaterials[0];
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        //Changement forme
        ChoosingForme();
        
        
        
        
        if (!hasCore) //attirance des entités vers sois meme si je ne fais pas parti d'un groupe
        {
            GetComponentInChildren<SphereCollider>().enabled = true;
            
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
            GetComponentInChildren<SphereCollider>().enabled = false;
        }
        


        if (inMyGroup.Count > minCoreParticle -1 && !hasCore && _instCore == null) //toujours laisser -1 pour décompter le possèsseur
        {
            _instCore = Instantiate(core, new Vector3(transform.position.x, 0.2f, transform.position.z), Quaternion.identity);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && !hasCore)
        {
            isInGroups = false;
            
        }
    }

    void ChoosingForme()
    {
        if (forme == Forme.Cube)
        {
            _myMesh = cubeMesh;
            gameObject.name = "Cube";
        }
        if (forme == Forme.Cone)
        {
            _myMesh = coneMesh;
            gameObject.name = "Cone";
        }
        if (forme == Forme.Triangle)
        {
            _myMesh = triangleMesh;
            gameObject.name = "Triangle";
        }
        if (forme == Forme.Icosphere)
        {
            _myMesh = icosphereMesh;
            gameObject.name = "Icosphere";
        }
        
        GetComponent<MeshFilter>().mesh = _myMesh;
        GetComponent<MeshCollider>().sharedMesh = _myMesh;
        
    }

    
}
