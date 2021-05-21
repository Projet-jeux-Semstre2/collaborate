using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimation : MonoBehaviour
{

    public Animator myAnimator;

    public AmeliorationManager Manager;

    public void Close()
    {
        Manager.close();
    }

    public void desac()
    {
        Manager.LastClose();
    }
    
    
    
}
