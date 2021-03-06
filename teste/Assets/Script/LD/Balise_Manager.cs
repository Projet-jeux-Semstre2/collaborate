﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balise_Manager : MonoBehaviour
{

    public GameObject[] balises;

    public float nbBaliseFinish;

    public bool allBaliseFinish;
    
   
    void OnEnable()
    {

        int rdSequence = Random.Range(0, 10);
        if (rdSequence < 5)
        {
            BaliseSequence.sequencePlay = 1;
            print("sequence Play 1");
            balises[0].GetComponent<BaliseSequence>().leaderSequence = true;
            balises[1].GetComponent<BaliseSequence>().leaderSequence = true;
            balises[9].GetComponent<BaliseSequence>().leaderSequence = true;
            
        }

        if (rdSequence >= 5 && rdSequence < 10)
        {
            BaliseSequence.sequencePlay = 2;
            print("sequence Play 2");
            balises[0].GetComponent<BaliseSequence>().leaderSequence = true;
            balises[9].GetComponent<BaliseSequence>().leaderSequence = true;
            balises[2].GetComponent<BaliseSequence>().leaderSequence = true;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var baliseEnd in balises)
        {
            Balise_Fonctionnement baliseFonctionnement = baliseEnd.GetComponent<Balise_Fonctionnement>();
            if (baliseFonctionnement.isCapture && !baliseFonctionnement.isCount)
            {
                baliseFonctionnement.isCount = true;
                nbBaliseFinish++;
            }
        }

        if (nbBaliseFinish == balises.Length)
        {
            allBaliseFinish = true;
        }

        if (allBaliseFinish)
        {
            SceneManager.LoadScene("Win");
        }

        
        
        
    }
    
    
    


    
    
    
    
}
