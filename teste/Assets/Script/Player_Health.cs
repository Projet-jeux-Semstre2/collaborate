using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player_Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    

    public GameObject Damage_Fb;
    public float fb_time = 0.2f;

    
    
    [Space(50)]

    public GameObject coreRecup;

    public GameManager _gameManager;

    private bool debugSunnyLinvincible;

    public Text healthText;
    private void Start()
    {
        healthText = GameObject.FindWithTag("HealthUi").GetComponent<Text>();
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
        healthText.text = "Health : " + health;
        
        if (health <= 0)
        {
            Die();
        }

        if (health >= maxHealth && !debugSunnyLinvincible)
        {
            health = maxHealth;
        }
        
        
        //Debug sunny
        if (Input.GetKeyDown(KeyCode.I) && !debugSunnyLinvincible)
        {
            health = 100000000f;
            debugSunnyLinvincible = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && debugSunnyLinvincible)
        {
            health = maxHealth;
            debugSunnyLinvincible = false;
        }

    }

    public IEnumerator HurtFb()
    {
        Damage_Fb.SetActive(true);
        
        yield return new WaitForSeconds(fb_time);
        
        Damage_Fb.SetActive(false);
    }

    void Die()
    {
        _gameManager.GetComponent<GameManager>().GameOver();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
