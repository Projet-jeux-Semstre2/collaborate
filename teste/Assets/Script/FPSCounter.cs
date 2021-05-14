using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSCounter : MonoBehaviour
{
    public int FPS { get; private set; }

    public GameObject loadScreen;
    public bool inLoad = true;

    private void OnEnable()
    {
        //loadScreen.SetActive(true);
        //StartCoroutine(Load());
    }

    void Update ()
    {
        Application.targetFrameRate = 144;
        //FPS = (int)(1f / Time.unscaledDeltaTime);
    }

    IEnumerator Load()
    {
        inLoad = true;
        while (FPS < 60)
        {
            loadScreen.SetActive(true);
            yield return null;
        }
        
        loadScreen.SetActive(false);
        inLoad = false;
    }
}
