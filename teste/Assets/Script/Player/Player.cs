﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [Header("Speed à changé")]
    public float initWalkSpeed = 9;
    [Header("Speed in Real Time")]
    public float walkingSpeed;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    public GameObject Groupe;
    
    
    
    
    // ici c'est le sound
    public string fmodjumpPlayer;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 inputVector = Vector3.zero;
    float rotationX = 0;

    //[HideInInspector]
    public bool canMove = true;
    public bool canDoubleJump = false;
    public bool StopJump = false;

    public bool sonPlay= false;

    public GameObject useButton;
    public bool useButtonOn;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // Lock cursor
        PauseMenu.cursorLock = true;

        walkingSpeed = initWalkSpeed;
    }

    void Update()
    {

        if (useButtonOn)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
        

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run

        
        float movementDirectionY;
        
        inputVector.z = canMove ?  walkingSpeed* Input.GetAxis("Vertical") : 0;
        inputVector.x = canMove ? walkingSpeed* Input.GetAxis("Horizontal") : 0;
        movementDirectionY = moveDirection.y;
        moveDirection = (forward * inputVector.z) + (right * inputVector.x);
        
        
        
        //Jump
        if (characterController.isGrounded)
        {
            canDoubleJump = false;
            StopJump = false;
        }


        if (!characterController.isGrounded && !StopJump)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded)
        { 
            moveDirection.y = jumpSpeed; 
            // ici c'est le sound , provisoire,
           FMODUnity.RuntimeManager.PlayOneShot(fmodjumpPlayer);
           
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (Input.GetButtonDown("Jump") && canMove && !characterController.isGrounded && canDoubleJump && !StopJump)
        {
            moveDirection.y = jumpSpeed;
            StopJump = true;
            // ici c'est le sound , provisoire,
            FMODUnity.RuntimeManager.PlayOneShot(fmodjumpPlayer);
            
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        /*if (!characterController.isGrounded && Input.GetKeyDown(KeyCode.A))
        {
            moveDirection.y -= (gravity*5) * Time.deltaTime;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }*/

        

        moveDirection.y -= gravity * Time.deltaTime;
        if(!characterController.isGrounded)
        {
            //moveDirection.y -= gravity * Time.deltaTime;
        }

        if (characterController.isGrounded && moveDirection.y <= -5f)
        {
            moveDirection.y = 0;
        }
        
        
        //Clamp de la gravité
        if (moveDirection.y < -gravity)
        {
            moveDirection.y = -gravity;
        }
        

        



        

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove && !PauseMenu.GameIsPaused)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

       
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
