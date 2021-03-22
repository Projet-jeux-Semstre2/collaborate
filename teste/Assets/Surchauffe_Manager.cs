using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surchauffe_Manager : MonoBehaviour
{
    public float surchauffe;
    public float niveauSurchauffe;

    public float t_forLooseSurchauffe;
    
    private float t;
    private void Update()
    {
        t += Time.deltaTime;
        if (niveauSurchauffe >= 1 && t >= t_forLooseSurchauffe)
        {
            niveauSurchauffe--;
            t = 0;
        }
    }
}
