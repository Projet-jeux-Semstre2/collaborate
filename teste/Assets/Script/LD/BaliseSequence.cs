using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaliseSequence : MonoBehaviour
{
    
    public Animator[] myDoors;
    public bool leaderSequence;

    public Balise_Fonctionnement[] _baliseFonctionnement;

    private void Start()
    {
        if (leaderSequence)
        {
            _baliseFonctionnement[0].canBeActive = true;
        }
    }


    private void Update()
    {
        if(leaderSequence)
        {
            for (int i = 0; i < _baliseFonctionnement.Length -1 ; i++)
            {
                if(_baliseFonctionnement[i].isCapture && !_baliseFonctionnement[i+1].canBeActive)
                {
                    _baliseFonctionnement[i+1].canBeActive = true;
                    print(_baliseFonctionnement[i+1].name+ "canBeActive");
                }
            }
        }

        if (_baliseFonctionnement[0].isCapture && _baliseFonctionnement[0].closeDoor)
        {
            myDoors[0].SetBool("Open", false);
            myDoors[1].SetBool("Open", false);
            myDoors[2].SetBool("Open", false);
        }
        
        
    }
}
