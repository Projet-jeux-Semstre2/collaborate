using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player_Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    

    public GameObject Damage_Fb;
    public float fb_time = 0.2f;

    public GameObject coreRecup;
    

    private void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ennemiProjectile"))
        {
            health -= coreRecup.GetComponent<Core_Attack>().distDamage;
            StartCoroutine(HurtFb());
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        
    }

    public IEnumerator HurtFb()
    {
        Damage_Fb.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        
        Damage_Fb.SetActive(false);
    }

    void Die()
    {
        SceneManager.LoadScene(0);
    }
}
