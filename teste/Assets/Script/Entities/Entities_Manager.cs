using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    
    
    
    
    public enum Forme {Cube, Cone, Triangle, Icosphere}
    [Header("Choix de la forme")]
    [Tooltip("Choisis la forme que tu veut pour cette entité")]
    public Forme forme;
    public Mesh cubeMesh;
    public Mesh coneMesh;
    public Mesh triangleMesh;
    public Mesh icosphereMesh;
    
    private Mesh _myMesh;


    [Header("Stats générale inGame")] 
    public float speed;
    public float attackDamage;
    public float health = 1.0f;

    //Init stats
    private float _initSpeed;
    private float _initAD;
    private float _initHealth;

    [Header("Tank Stats")] 
    [SerializeField]private float t_speed;
    [SerializeField]private float t_attackDamage;
    [SerializeField]private float t_health;

    [Header("Vif Stats")] 
    [SerializeField]private float v_speed;
    [SerializeField]private float v_attackDamage;
    [SerializeField]private float v_health;

    [Header("Créateur Stats")] 
    [SerializeField]private float c_speed;
    [SerializeField]private float c_attackDamage;
    [SerializeField]private float c_health;

    [Header("Aggresif Stats")] 
    [SerializeField]private float a_speed;
    [SerializeField]private float a_attackDamage;
    [SerializeField]private float a_health;


    private void OnEnable()
    {
        pullRadius = Random.Range(1, 7);
        pullForce = Random.Range(pullForceMin, pullForceMax);
        GetComponent<MeshRenderer>().material = ChangeMaterials[0];
        _rb = GetComponent<Rigidbody>();
        
        
        //Stats
        
        ChoosingForme();
        health = _initHealth;
        speed = _initSpeed;
        attackDamage = _initAD;
    }



    

    public void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime*speed);
        
        
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
        if (forme == Forme.Cube)//Tank
        {
            _myMesh = cubeMesh;
            gameObject.name = "Cube";
            
            //Stats
            _initHealth = t_health;
            _initSpeed = t_speed;
            _initAD = t_attackDamage;


        }
        if (forme == Forme.Cone)//Vif
        {
            _myMesh = coneMesh;
            gameObject.name = "Cone";
            
            //Stats
            _initHealth = v_health;
            _initSpeed = v_speed;
            _initAD = v_attackDamage;

        }
        if (forme == Forme.Triangle)//Aggressif
        {
            _myMesh = triangleMesh;
            gameObject.name = "Triangle";
            
            //Stats
            _initHealth = a_health;
            _initSpeed = a_speed;
            _initAD = a_attackDamage;
        }
        if (forme == Forme.Icosphere)//Créateur
        {
            _myMesh = icosphereMesh;
            gameObject.name = "Icosphere";
            
            //Stats
            _initHealth = c_health;
            _initSpeed = c_speed;
            _initAD = c_attackDamage;
        }
        
        GetComponent<MeshFilter>().mesh = _myMesh;
        GetComponent<MeshCollider>().sharedMesh = _myMesh;
        
    }

    
}
