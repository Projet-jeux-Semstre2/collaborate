using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseDestroyennemis : MonoBehaviour
{
    public Balise_Fonctionnement BaliseFonctionnement;
    public List<GameObject> ennemisIn;


    private void OnTriggerExit(Collider other)
    {
        if (ennemisIn.Contains(other.gameObject) && other.CompareTag("ennemis"))
        {
            ennemisIn.Remove(other.gameObject);
        }
        
    }

    private void Update()
    {
        for (int i = ennemisIn.Count - 1; i > -1 ; i--)
        {
            if (ennemisIn[i] == null)
            {
                ennemisIn.RemoveAt(i);
            }
        }

        if(BaliseFonctionnement.closeDoor && ennemisIn.Count > 0)
        {
            foreach (var entities in ennemisIn)
            {
                Destroy(entities);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ennemis") && !ennemisIn.Contains(other.gameObject))
        {
            ennemisIn.Add(other.gameObject);
        }
    }

}
