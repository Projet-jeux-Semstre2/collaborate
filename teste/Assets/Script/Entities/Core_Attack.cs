using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Attack : MonoBehaviour
{

    private Core_Manager _coreManager;
    
    public bool canCacAttack = false;
    public bool isCaCAttack;
    
    public float cacCoolDown;

    private GameObject _player;
    private Player_Health _playerHealth;

    public float distance, distanceAttack;

    public float cacDamage = 2f;

    public float MaxCacDamage = 30f;
    
     private float buffDistAttack;
     public float temporairBase;
// les variables qui gère l'évolution de la distance attack
     public float minEvolution;
     public float coefTailleDistanceAttack;
     
     



    private void Start()
    {
        _coreManager = GetComponent<Core_Manager>();
        _player = _coreManager.player;
        _playerHealth = _player.GetComponent<Player_Health>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position,  new Vector3(transform.position.x + temporairBase, transform.position.y, transform.position.z),Color.green);
        
        if (cacDamage >= MaxCacDamage)
        {
            cacDamage = MaxCacDamage;
        }
// valeur de qui va prendre le Nrb d'entité et s'ajouter a distance Attack 
        int nrbEntite = _coreManager.myEntities.Count;
       
        if (nrbEntite>minEvolution)
        { 
            buffDistAttack = nrbEntite/coefTailleDistanceAttack;
        }
        else
        {
            buffDistAttack = 0;
        }

       
        temporairBase = distanceAttack + buffDistAttack;

        distance = Vector3.Distance(transform.position, _player.transform.position);
        
        if (distance <= temporairBase && !isCaCAttack)
        {
            StartCoroutine(CaCAttack());
        }
    }

    

    public IEnumerator CaCAttack()
    {
        _playerHealth.health -= cacDamage;
        isCaCAttack = true;

        StartCoroutine(_playerHealth.HurtFb());

        yield return new WaitForSeconds(cacCoolDown);

        isCaCAttack = false;
        
        yield return null;
    }
    
}
