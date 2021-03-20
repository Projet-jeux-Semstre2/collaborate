using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeMenue : MonoBehaviour
{
    public int facileNombre = 30;
    public int dificileNombre = 90;
    public float nombreFinale;
    public SetUPScene _setUpScene;
    public float nrb;
    private void Start()
    {
         nrb = _setUpScene.GetComponent<SetUPScene>().NombreEntités;
    }

   
    
}
