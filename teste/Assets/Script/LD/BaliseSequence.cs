using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseSequence : MonoBehaviour
{
    
    public Animator[] myDoors;
    public bool leaderSequence;
    static public int sequencePlay; 
    public Balise_Fonctionnement[] baliseFonctionnement;
    public bool sequenceFinish;
    
    [Header("Sequence 1")]
    public Balise_Fonctionnement[] baliseSequence1;

    [Header("Sequence 2")]
    public Balise_Fonctionnement[] baliseSequence2;

    


    private void Start()
    {
        switch (sequencePlay)
        {
            case 1:
                ChooseSequence(baliseSequence1);
                break;
            case 2:
                ChooseSequence(baliseSequence2);
                break;
        }
        
        
        
        if (leaderSequence)
        {
            baliseFonctionnement[0].canBeActive = true;
        }
    }


    private void Update()
    {
        if(leaderSequence)
        {
            for (int i = 0; i < baliseFonctionnement.Length -1 ; i++)
            {
                if(baliseFonctionnement[i].isCapture && !baliseFonctionnement[i+1].canBeActive)
                {
                    baliseFonctionnement[i+1].canBeActive = true;
                    print(baliseFonctionnement[i+1].name+ "canBeActive");
                }
            }
        }

        if (GetComponent<Balise_Fonctionnement>().isCapture && GetComponent<Balise_Fonctionnement>().closeDoor)
        {
            foreach (var doors in myDoors)
            {
                doors.SetBool("Open", false);
            }
        }
    }

    void ChooseSequence(Balise_Fonctionnement[] baliseChange)
    {
        baliseFonctionnement = baliseChange;
    }
}
