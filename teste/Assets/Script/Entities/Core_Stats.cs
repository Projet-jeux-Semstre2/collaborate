using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Stats : MonoBehaviour
{
    private Core_Manager _coreManager;
    private Core_Attack _coreAttack;

    private Entities_Stats _entitiesStats;
    

    [Tooltip("la speed de chaque entités est divisé par ce nombre avant d'être ajouter au core")]
    public float speedDiviseur = 5;
    
    [Tooltip("les damages de chaque entités est divisé par ce nombre avant d'être ajouter au core")]
    public float damageDiviseur = 5;

    private void OnEnable()
    {
        _coreAttack = GetComponent<Core_Attack>();
        _coreManager = GetComponent<Core_Manager>();
    }
    
    public void UpStats(GameObject obj)
    {
        _coreManager.agent.speed += obj.GetComponent<Entities_Stats>().speed / speedDiviseur;
        _coreAttack.cacDamage += obj.GetComponent<Entities_Stats>().damage / damageDiviseur;
        _coreAttack.distDamage += obj.GetComponent<Entities_Stats>().damage / damageDiviseur;
    }

    public void SuppStats(GameObject obj)
    {
        _coreManager.agent.speed -= obj.GetComponent<Entities_Stats>().speed / speedDiviseur;
        _coreAttack.cacDamage -= obj.GetComponent<Entities_Stats>().damage / damageDiviseur;
        _coreAttack.distDamage -= obj.GetComponent<Entities_Stats>().damage / damageDiviseur;
    }

    public void BuffEntitesUp(GameObject obj, float difficulty)
    {
        float buffstats_Speed = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;
        float buffstats_Damage = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;
        float buffstats_Health = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;

        foreach (GameObject entities in _coreManager.myEntities)
        {
            if (entities != obj)
            {
                entities.GetComponent<Entities_Stats>().speed += buffstats_Speed;
                entities.GetComponent<Entities_Stats>().damage += buffstats_Damage;
                entities.GetComponent<Entities_Stats>().health += buffstats_Health;
            }
        }
    }
    
    public void BuffEntitesSupp(GameObject obj, float difficulty)
    {
        float buffstatsSupp_Speed = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;
        float buffstatsSupp_Damage = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;
        float buffstatsSupp_Health = obj.GetComponent<Entities_Stats>().speed / _coreManager.myEntities.Count * difficulty;

        foreach (GameObject entities in _coreManager.myEntities)
        {
            if (entities != obj)
            {
                entities.GetComponent<Entities_Stats>().speed -= buffstatsSupp_Speed;
                entities.GetComponent<Entities_Stats>().damage -= buffstatsSupp_Damage;
                entities.GetComponent<Entities_Stats>().health -= buffstatsSupp_Health;
            }
        }
    }
}
