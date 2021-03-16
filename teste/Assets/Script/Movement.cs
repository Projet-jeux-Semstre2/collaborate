using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;
    
    private Rigidbody _rig;
    private bool _isGrounded;
    
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float moveForward = Input.GetAxis("Vertical") * speed;
        float moveSide = Input.GetAxis("Horizontal") * speed;
        float moveUp = Input.GetAxis("Jump") * jumpForce;


        _rig.velocity = (transform.forward * moveForward) + (transform.right * moveSide) +
                        (transform.up * _rig.velocity.y);


        if (_isGrounded && moveUp != 0)
        {
            _rig.AddForce(transform.up * moveUp, ForceMode.VelocityChange);
            _isGrounded = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            _isGrounded = false; 
        }
    }
}
