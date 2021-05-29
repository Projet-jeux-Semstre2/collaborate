using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Core_Comportement : MonoBehaviour
{
    private Core_Manager _coreManager;
    private Core_Attack _coreAttack;

    public float patrolRadius;
    private bool canDoTarget = true;
    
    
    

    public Vector3 fearPoint;
    

    private Color _colorLine;
    public LayerMask layerMaskRaycast;
    public float raycastDist;
    public bool canChase;
    


    private void OnEnable()
    {
        _coreManager = GetComponent<Core_Manager>();
        _coreAttack = GetComponent<Core_Attack>();
    }


    private void Update()
    {
        ComportementPalier();
        
        
        if(_coreManager.target != _coreManager.player.transform.position) // patrol
        {
            _colorLine = Color.magenta;
        }
        if (_coreManager.target == _coreManager.player.transform.position) // traque le joueur
        {
            _colorLine = Color.red;
        }

        if (_coreManager.target != null)
        {
            Debug.DrawLine(transform.position, _coreManager.target, _colorLine);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_coreManager.palier == "moyen" || _coreManager.palier == "grand")
            {
                canChase = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _coreManager.palier == "moyen")
        {
            canChase = false;
        }
    }


    void CreatePatrolTarget()
    {
        
        Vector3 rdPosition = Random.insideUnitSphere * patrolRadius + transform.position;
        rdPosition.y = transform.position.y;
        
        canDoTarget = false;
        

        RaycastHit hit;

        if (Physics.Raycast(rdPosition, Vector3.down, out hit, raycastDist, layerMaskRaycast) && !Physics.Raycast(rdPosition, Vector3.up, out hit, raycastDist, layerMaskRaycast))
        {
            _coreManager.target = rdPosition;
        }
       
        
        
        Debug.DrawRay(rdPosition, Vector3.down, Color.green, raycastDist);
        Debug.DrawRay(rdPosition, Vector3.up, Color.yellow, raycastDist);
        
    }

    void Patrol()
    {
        if ( _coreManager.agent.enabled &&  !canDoTarget )
        {
            if (Vector3.Distance(_coreManager.target, transform.position) <= 1.2f && _coreManager.agent.isActiveAndEnabled)
            {
                canDoTarget = true;
            }
            
        }
        
        
    }


    void ChasePlayer()
    {
        _coreManager.target = _coreManager.player.transform.position;
    }

     
    

    
    
    
    void ComportementPalier()
    {
        switch (_coreManager.palier)
        {

            case "moyen":
                if(canDoTarget)
                {
                    CreatePatrolTarget();
                }

                if (!canChase)
                {
                    Patrol();
                }

                if (canChase)
                {
                    ChasePlayer();
                }
                
                
                break;
            
            case "grand":
                ChasePlayer();
              
                break;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(fearPoint, 1f);
    }
}
