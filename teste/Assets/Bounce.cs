using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody _rb;
    public float str = 0.21f;
   
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _rb.AddForce(_rb.velocity* str, ForceMode.Impulse);
        }
        
    }
}
