using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balise_ActiveChase : MonoBehaviour
{
    [SerializeField] private List<GameObject> chaseActiv = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("core") && !chaseActiv.Contains(other.gameObject))
        {
            other.GetComponent<Core_Comportement>().canChase = true;
            chaseActiv.Add(other.gameObject);
        }
    }
    
    

    private void Update()
    {
        for (int i = chaseActiv.Count - 1; i > -1 ; i--)
        {
            if (!chaseActiv[i])
            {
                chaseActiv.RemoveAt(i);
                
            }
        }
    }


    private void OnDisable()
    {
        if (chaseActiv.Count > 0)
        {
            for (int i = 0; i < chaseActiv.Count; i++)
            {
                if (chaseActiv[i].GetComponent<Core_Manager>().palier == "moyen" &&chaseActiv[i].CompareTag("core") && chaseActiv.Contains(chaseActiv[i]))
                {
                    chaseActiv[i].GetComponent<Core_Comportement>().canChase = false;
                }
                chaseActiv.Remove(chaseActiv[i]);
            }
            {
                
            }
        }
        
    }
}
