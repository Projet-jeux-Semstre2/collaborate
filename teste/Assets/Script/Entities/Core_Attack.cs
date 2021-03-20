using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Attack : MonoBehaviour
{

    private Core_Manager _coreManager;
    
    public GameObject attackProjectile;
    public float Force;
    public GameObject launchPoint;

    public bool isDIstAttack;
    public bool canCacAttack = false;
    public bool isCaCAttack;

    public float distCoolDown;
    public float cacCoolDown;

    private GameObject _player;
    private Player_Health _playerHealth;

    public float cacDamage = 2f;
    public float distDamage = 10f;
    



    private void Start()
    {
        _coreManager = GetComponent<Core_Manager>();
        _player = _coreManager.player;
        _playerHealth = _player.GetComponent<Player_Health>();
    }

    private void Update()
    {
        if (canCacAttack && !isCaCAttack)
        {
            StartCoroutine(CaCAttack());
        }
    }

    public IEnumerator DistAttack()
    {
        GameObject projectile = Instantiate(attackProjectile, launchPoint.transform.position, Quaternion.identity);
        
        
        projectile.GetComponent<Rigidbody>().AddForce(launchPoint.transform.forward * Force, ForceMode.Impulse) ;
        
        
        isDIstAttack = true;
        
        yield return new WaitForSeconds(distCoolDown);

        isDIstAttack = false;
    }

    public IEnumerator CaCAttack()
    {
        _playerHealth.health -= cacDamage;
        isCaCAttack = true;
        print("cac attack");

        StartCoroutine(_playerHealth.HurtFb());

        yield return new WaitForSeconds(cacCoolDown);

        isCaCAttack = false;
        
        yield return null;
    }
    
}
