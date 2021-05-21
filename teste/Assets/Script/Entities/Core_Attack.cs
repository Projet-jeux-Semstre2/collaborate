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




    private void Start()
    {
        _coreManager = GetComponent<Core_Manager>();
        _player = _coreManager.player;
        _playerHealth = _player.GetComponent<Player_Health>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, _player.transform.position);
        
        if (distance <= distanceAttack && !isCaCAttack)
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
