using System;
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

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ennemis") && !other.GetComponent<Entities_Manager>().hasCore)
        {
            myEntities.Add(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = true;
            other.GetComponent<Entities_Manager>().isInGroups = true;
            other.GetComponent<Entities_Manager>().myCore = gameObject;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ennemis") && other.GetComponent<Entities_Manager>().hasCore)
        {
            myEntities.Remove(other.gameObject);
            other.GetComponent<Entities_Manager>().hasCore = false;
            other.GetComponent<Entities_Manager>().isInGroups = false;
        }
    }
    

    private void OnDisable()
    {
        foreach (var obj in myEntities)
        {
            obj.GetComponent<Entities_Manager>().hasCore = false;
            obj.GetComponent<Entities_Manager>().isInGroups = false;
        }
    }


    private void FixedUpdate()
    {
        GetComponent<SphereCollider>().radius = pullRadius * 3.33f;
        
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius) )
        {
            if (collider.gameObject.CompareTag("ennemis") && myEntities.Contains(collider.gameObject))
            {
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                
            }
        }

        GetComponent<NavMeshAgent>().destination = _player.transform.position;
        
        
        //core soit toujours au centre de tout les objt compris dans myEntities

        //transform.position = (myEntities[0].transform.localPosition + myEntities[1].transform.localPosition + myEntities[2].transform.localPosition + myEntities[3].transform.localPosition)/4 ;

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
