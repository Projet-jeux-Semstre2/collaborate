using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUPScene : MonoBehaviour
{
    public ChallengeMenue _challengeMenue;
    
    public float NombreEntités;
    // Start is called before the first frame update
    

    private void Update()
    {
        NombreEntités = _challengeMenue.GetComponent<ChallengeMenue>().nombreFinale;
    }
}
