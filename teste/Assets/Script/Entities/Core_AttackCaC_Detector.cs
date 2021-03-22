using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_AttackCaC_Detector : MonoBehaviour
{
    private Core_Attack _coreAttack;


    private void Start()
    {
        _coreAttack = GetComponentInParent<Core_Attack>();
    }


    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            _coreAttack.canCacAttack = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _coreAttack.canCacAttack = false;
        }
    }
}
