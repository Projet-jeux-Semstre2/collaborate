using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Surchauffe_Manager : MonoBehaviour
{
    public float surchauffe;
    public float niveauSurchauffe;

    public float t_forLooseSurchauffe;

    public RectTransform surchauffeImage;
    
    // LE SONS
    public string FmodSurchauffe;
    public string FmodCooling;

    private float t;
    private bool isSurchauffeMax = false;
    

    private void Update()
    {
        float add = 1115 / surchauffe;
        if (niveauSurchauffe > 0)
        {
            surchauffeImage.sizeDelta = Vector2.Lerp(surchauffeImage.sizeDelta, new Vector2(add * niveauSurchauffe, surchauffeImage.sizeDelta.y), Time.time);
        }
        
        
        t += Time.deltaTime;
        if (niveauSurchauffe >= 1 && t >= t_forLooseSurchauffe)
        {
            FMODUnity.RuntimeManager.PlayOneShot(FmodCooling, GetComponent<Transform>().position);
            niveauSurchauffe--;
            t = 0;
        }

        if (niveauSurchauffe >= surchauffe) // lance le sons de surchauffe max une seule foix
        {
            if (!isSurchauffeMax)
            {
                SurchauffeMax();
            }
        }
    }
    
    
    void SurchauffeMax() // quand on atteint la surchauffe max
    {
        FMODUnity.RuntimeManager.PlayOneShot(FmodSurchauffe, GetComponent<Transform>().position);
        isSurchauffeMax = true;
    }
}
