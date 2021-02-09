﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Core_Manager : MonoBehaviour
{
    public List<GameObject> myEntities;

    public float pullRadius;
    public float pullForce;

    private Vector3 _moyennePos;

    private GameObject _player;

    private NavMeshAgent _agent;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ennemis") && !other.GetComponent<Entities_Manager>().hasCore && other.GetComponent<Entities_Manager>().myCore == null)
        {
            myEntities.Add(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = true;
            other.GetComponent<Entities_Manager>().isInGroups = true;
            other.GetComponent<Entities_Manager>().myCore = gameObject;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && other.GetComponent<Entities_Manager>().hasCore && other.GetComponent<Entities_Manager>().myCore == gameObject)
        {
            myEntities.Remove(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = false;
            other.GetComponent<Entities_Manager>().isInGroups = false;
            other.GetComponent<Entities_Manager>().myCore = null;
        }
    }
    

    private void OnDisable()
    {
        foreach (var obj in myEntities)
        {
            obj.GetComponent<Entities_Manager>().hasCore = false;
            obj.GetComponent<Entities_Manager>().isInGroups = false;
            obj.GetComponent<Entities_Manager>().myCore = null;
        }
    }


    private void FixedUpdate()
    {
        GetComponent<SphereCollider>().radius = pullRadius * 3.33f;
        
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

        if (_agent.enabled)
        {
            _agent.destination = _player.transform.position;
        }
        
        
        
        
        _moyennePos = Vector3.zero;
        for (int i = 0; i < myEntities.Count; i++)
        {
            _moyennePos += myEntities[i].transform.localPosition;
        }

        //transform.position = _moyennePos/myEntities.Count;
    }

    private void LateUpdate()
    {
        if (myEntities.Count <= 2)
        {
            Destroy(gameObject);

        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
