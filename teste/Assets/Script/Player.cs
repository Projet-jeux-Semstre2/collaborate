using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;
    

    
    
    
    // ici c'est le sound
    public string fmodWalkPlayer;
    public string fmodjumpPlayer;

    CharacterController characterController;
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //[HideInInspector]
    public bool canMove = true;
    public bool canDoubleJump = false;
    public bool StopJump = false;
    public bool Dead = false;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        
        
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run

        float curSpeedX;
        float curSpeedY;
        float movementDirectionY;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        
        
        
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
        if (!characterController.isGrounded && Input.GetKeyDown(KeyCode.A))
        {
            moveDirection.y -= (gravity*5) * Time.deltaTime;
            Debug.Log("Down");
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        
        
        //Clamp de la gravité
        if (moveDirection.y <= -gravity)
        {
            moveDirection.y = -gravity;
        }

        



        

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

       
    }

    

    

}
