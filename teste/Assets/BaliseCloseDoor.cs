using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseCloseDoor : MonoBehaviour
{
    public Balise_Fonctionnement BaliseFonctionnement;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && BaliseFonctionnement.isCapture)
        {
            BaliseFonctionnement.closeDoor = true;
            //destroy all entities in
        }
    }
}
