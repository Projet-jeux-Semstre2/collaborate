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

    public float currentStamina;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
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
        
        

        // Stamina Run
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            Stamina.instance.UseStamina(0.05f);
        }*/

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
           // Stamina.instance.UseStamina(10);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (Input.GetButtonDown("Jump") && canMove && !characterController.isGrounded && canDoubleJump && !StopJump)
        {
            moveDirection.y = jumpSpeed;
            StopJump = true;
            //Stamina.instance.UseStamina(15);
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



        /*else if (!characterController.isGrounded && Dead)
        {
            gameObject.transform.position = new Vector3(6.67000008f, 17.3500004f, -42.6500015f);
        }
        */

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

        /* //Shooting
        if (Input.GetMouseButton(0))
        {
            //On instancie la balle à la position et la rotation du launchPoint
            //On stocke cette instantiation dans une variable bulletInstance
            GameObject bulletInstance = Instantiate(bulletPrefab, launchPoint.position, launchPoint.rotation);

            //Pour appliquer une force par script, il va falloir faire appel à une méthode du RigidBody,
            //on le récupère donc dans une variable :
            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();

            //On ajoute la force a la variable Rigibody
            bulletRB.AddForce(launchPoint.up * launchForce, ForceMode.Impulse);

        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Dead = true;
        }
    }

    /*private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("RunningWall"))
        {
            StopJump = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("RunningWall") && !StopJump)
        {
            StopJump = false;
        }
    }*/

}
