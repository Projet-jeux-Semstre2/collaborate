using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class Core_Manager : MonoBehaviour
{
    public List<GameObject> myEntities;

    public float pullRadius;
    public float pullForce;

    private Vector3 _moyennePos;
    public GameObject player;
    public NavMeshAgent agent;
    
    
    
    
    private float t;

    private bool _isOnGround;

    public string palier;

    public Vector3 target;

    public GameObject renderer;
    private GameObject _instRenderer;

    private bool haveRender = false;


    private void OnEnable()
    {
        if (!haveRender)
        {
            _instRenderer = Instantiate(renderer, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            _instRenderer.GetComponent<Core_renderer>().core = transform;
            haveRender = true;
        }
        
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
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
        Destroy(_instRenderer);
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

        if (agent.enabled && _isOnGround && target != null)
        {
            agent.destination = target;

        }

        if (Physics.Raycast(transform.position, Vector3.down * 2))
        {
            _isOnGround = true;
        }
        else
        {
            _isOnGround = false;
        }

        if (_isOnGround && !agent.enabled)
        {
            agent.enabled = true;
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
        if (myEntities.Count <= 5)
        {
            palier = "petit";
        }
        if (myEntities.Count > 5 && myEntities.Count < 10)
        {
            palier = "moyen";
        }
        if (myEntities.Count >= 10)
        {
            palier = "grand";
        }
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
