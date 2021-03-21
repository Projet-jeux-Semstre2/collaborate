using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenue : MonoBehaviour
{
    public Text slideTextDficulté;
    public Slider sliderDificulté;
    public float minDificulties = 20;
    public float maxDificulties = 400;

    public void Start()
    {
        sliderDificulté.minValue = minDificulties;
        sliderDificulté.maxValue = maxDificulties;
    }

    private void Update()
    {
        slideTextDficulté.text = "nombre = " + sliderDificulté.value;
    }
}
