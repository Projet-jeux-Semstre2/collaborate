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

    private float t;

    

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
            niveauSurchauffe--;
            t = 0;
        }
        
        
        
        
    }
}
