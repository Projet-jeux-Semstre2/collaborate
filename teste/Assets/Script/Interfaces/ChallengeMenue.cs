using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeMenue : MonoBehaviour
{
    public int facileNombre = 30;
    public int dificileNombre = 90;
    public float nombreFinale;


    public void dificutynumber (float dificultyNombre)
    {
        nombreFinale = dificultyNombre;
    }
}
