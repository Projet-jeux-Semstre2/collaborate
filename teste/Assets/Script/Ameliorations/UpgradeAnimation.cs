using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimation : MonoBehaviour
{
    public bool close;

    public Animator myAnimator;

    public GameObject Manager;

    public void animationStart()
    {
        close = false;
    }

    public void Close()
    {
        close = true;
    }
    public void Desact()
    {
        Manager.SetActive(false);
    }
    
    
}
