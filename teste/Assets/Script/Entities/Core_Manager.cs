﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Core_Manager : MonoBehaviour
{
    public List<GameObject> myEntities;
    
    public float tankNb, agressifNb, vifNb, createurNb;
    

    private Core_Stats _coreStats;

    public float pullRadius;
    public float pullForce;

    private Vector3 _moyennePos;
    public GameObject player;
    public NavMeshAgent agent;

    public SphereCollider sphereColliderPull;
    
    private float t;

    public bool _isOnGround;

    public string palier;

    public Vector3 target;

    public GameObject renderer;
    private GameObject _instRenderer;

    private bool haveRender = false;
    public LayerMask layerMask;

    public float minimumEntities = 2;
    public float entitiesCap = 35;

    private float nbEntitéForRadius = 5f;

    public float difficultyLevel = 1;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _coreStats = GetComponent<Core_Stats>();
        if (!haveRender)
        {
            if (renderer)
            {
                _instRenderer = Instantiate(renderer, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                _instRenderer.GetComponent<Core_renderer>().core = transform;
                haveRender = true;
            }
        }

        target = transform.position;
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ennemis") && !other.GetComponent<Entities_Manager>().hasCore && other.GetComponent<Entities_Manager>().myCore == null && myEntities.Count <= entitiesCap)
        {
            myEntities.Add(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = true;
            other.GetComponent<Entities_Manager>().isInGroups = true;
            other.GetComponent<Entities_Manager>().myCore = gameObject;
            
            ColorTypeCount("Add", other.gameObject);
            
            _coreStats.UpStats(other.gameObject);
            _coreStats.BuffEntitesUp(other.gameObject, difficultyLevel);
            
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && other.GetComponent<Entities_Manager>().hasCore && other.GetComponent<Entities_Manager>().myCore == gameObject)
        {
            _coreStats.SuppStats(other.gameObject);
            myEntities.Remove(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = false;
            other.GetComponent<Entities_Manager>().isInGroups = false;
            other.GetComponent<Entities_Manager>().myCore = null;
            
            ColorTypeCount("Remove", other.gameObject);
            
            _coreStats.BuffEntitesSupp(other.gameObject, difficultyLevel);
        }
    }
    

    private void OnDestroy()
    {
        Destroy(_instRenderer);
        foreach (var obj in myEntities)
        {
            if (obj)
            {
                obj.GetComponent<Entities_Manager>().hasCore = false;
                obj.GetComponent<Entities_Manager>().isInGroups = false;
                obj.GetComponent<Entities_Manager>().myCore = null;
            }
            
        }
    }


    private void FixedUpdate()
    {

        if (nbEntitéForRadius <= myEntities.Count)
        {
            float nbentitésbeforeup = nbEntitéForRadius;
            pullRadius = pullRadius + 2;
            nbEntitéForRadius += nbentitésbeforeup;
        }
        
        sphereColliderPull.radius = pullRadius * 3.3f;
        
        
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius) )
        {
            if (collider.gameObject.CompareTag("ennemis") && myEntities.Contains(collider.gameObject) && collider.GetComponent<Entities_Manager>().myCore == gameObject)
            {
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                
            }
        }

        if (agent.enabled && _isOnGround && target != null)
        {
            agent.destination = target;
        }

        if(Physics.Raycast(transform.position,Vector3.down*10, 2, layerMask))
        {
            
            _isOnGround = true;
        }
        else
        {
            _isOnGround = false;
        }
        

        if (_isOnGround)
        {
            agent.enabled = true;
        }

        if (!_isOnGround )
        {
            agent.enabled = false;
        }
        

        for (int i = myEntities.Count - 1; i > -1 ; i--)
        {
            if (myEntities[i] == null)
            {
                myEntities.RemoveAt(i);
            }
        }
        
        Palier();

    }

    void Palier()
    {
        if (myEntities.Count > minimumEntities && myEntities.Count < 15)
        {
            palier = "moyen";
        }
        if (myEntities.Count >= 15)
        {
            palier = "grand";
        }
    }

    public void ColorTypeCount(string state, GameObject gameObject)
    {
        if (state == "Add")
        {
            switch (gameObject.GetComponent<Entities_Manager>().type)
            {
                case "Tank":
                    tankNb ++;
                    break;
                case "Agressif":
                    agressifNb++;
                    break;
                case "Vif":
                    vifNb++;
                    break;
                case "Createur":
                    createurNb++;
                    break;
            }
        }

        if (state == "Remove")
        {
            switch (gameObject.GetComponent<Entities_Manager>().type)
            {
                case "Tank":
                    tankNb--;
                    break;
                case "Agressif":
                    agressifNb--;
                    break;
                case "Vif":
                    vifNb--;
                    break;
                case "Createur":
                    createurNb--;
                    break;
            }
        }
    }

    
    
    
    
    

    private void LateUpdate()
    {
        if (myEntities.Count <= minimumEntities)
        {
            Destroy(gameObject);
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
