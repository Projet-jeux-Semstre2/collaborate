using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; // tableaux des armes enregistrés en component dans le player
    public int activeWeaponsID; // index des armes
    
    
    private void Start()
    {

        weapons = GetComponentsInChildren<Weapon>();/// récupère les armes qui sont en enfant
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false); // toute disable en game 
        }

        weapons[activeWeaponsID].gameObject.SetActive(true); // enable l'arme en main 
    }
    
    
    
    void Update()
    {
        if (Input.GetButtonDown("ChangeWeapon"))
        {
            Changeweapon();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            weapons[activeWeaponsID].Engage();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            weapons[activeWeaponsID].DisEngage();
        }
        
    }
    
    
    private void Changeweapon()
    {
        weapons[activeWeaponsID].gameObject.SetActive(false);
        activeWeaponsID++;
        if (activeWeaponsID >= weapons.Length)
        {
            activeWeaponsID = 0;
        }
        weapons[activeWeaponsID].gameObject.SetActive(true);
    }
    
}
