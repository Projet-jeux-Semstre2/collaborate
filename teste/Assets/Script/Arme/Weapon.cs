using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public float damage = 1.0f;
        
    public  virtual void Engage()
    {
        
    }

    public virtual void DisEngage()
    {
        
    }
}
