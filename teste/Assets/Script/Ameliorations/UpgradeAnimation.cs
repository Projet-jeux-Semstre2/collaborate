using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimation : MonoBehaviour
{
    public bool animationEnd;

    public Animator myAnimator;

    public GameObject Manager;
    


    public void animationEndChecker()
    {
        animationEnd = true;
    }

    public void animationStart()
    {
        animationEnd = false;
    }

    public void Close()
    {
        Manager.SetActive(false);
    }
    
    
}
