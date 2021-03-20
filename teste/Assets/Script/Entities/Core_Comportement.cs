using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Core_Comportement : MonoBehaviour
{
    private Core_Manager _coreManager;
    private Core_Attack _coreAttack;

    public float patrolRadius;
    private bool canDoTarget = true;
    
    public bool isFear;
    public bool canBeFear = true;

    public float fearCooldown;
    

    public Vector3 fearPoint;

    public bool canAttackDistance;

    private Color _colorLine;
    


    private void OnEnable()
    {
        _coreManager = GetComponent<Core_Manager>();
        _coreAttack = GetComponent<Core_Attack>();
    }


    private void Update()
    {
        ComportementPalier();
        
        if (_coreManager.agent.remainingDistance < 5f && isFear) //fin de la fuite
        {
            print("j'ai plus peur");
                
            StartCoroutine(fuiteCoolDown());
            isFear = false;
        }


        if (canAttackDistance && !_coreAttack.isDIstAttack)
        {
            StartCoroutine(_coreAttack.DistAttack());

        }

        if (isFear) //apeuré
        {
            _colorLine = Color.cyan;
        }
        else if(_coreManager.target != _coreManager.player.transform.position) // patrol
        {
            _colorLine = Color.magenta;
        }
        else if (_coreManager.target == _coreManager.player.transform.position) // traque le joueur
        {
            _colorLine = Color.red;
        }
        
        Debug.DrawLine(transform.position, _coreManager.target, _colorLine);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_coreManager.palier == "petit")
            {
                if (canBeFear)
                {
                    StartCoroutine(fuite(other)); 
                }
            }

            if (_coreManager.palier == "moyen" || _coreManager.palier == "grand")
            {
                canAttackDistance = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canAttackDistance = false;
        }
    }


    void CreatePatrolTarget()
    {
        Vector3 rdPosition = Random.insideUnitSphere * patrolRadius;
        rdPosition.y = transform.position.y;
        

        _coreManager.target = rdPosition;
        
        canDoTarget = false;
    }

    void Patrol()
    {
        if (_coreManager.agent.remainingDistance <= 2 && !canDoTarget)
        {
            canDoTarget = true;
        }
        
        
    }


    void ChasePlayer()
    {
        _coreManager.target = _coreManager.player.transform.position;
    }

     
    

    IEnumerator fuite(Collider other)
    {
        isFear = true;
        canBeFear = false;
        canDoTarget = false;
        
        
        fearPoint =  other.transform.forward * 150;//fearCollider.fearPoint(transform.position);
        fearPoint.y = transform.position.y;

        _coreManager.target = fearPoint;
        
        yield return null;
    }

    private IEnumerator fuiteCoolDown()
    {
        canBeFear = false;
        
        yield return new WaitForSeconds(fearCooldown);

        canBeFear = true;
    }
    
    
    void ComportementPalier()
    {
        switch (_coreManager.palier)
        {
            case "petit":
                if(canDoTarget)
                {
                    CreatePatrolTarget();
                }

                if(!isFear)
                {
                    Patrol();
                }
                break;
            
            case "moyen":
                if(canDoTarget)
                {
                    CreatePatrolTarget();
                }

                if (!canAttackDistance)
                {
                    Patrol();
                }

                if (canAttackDistance)
                {
                    ChasePlayer();
                }
                
                
                break;
            
            case "grand":
                ChasePlayer();
              
                break;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(fearPoint, 1f);
    }
}
