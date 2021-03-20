using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    CharacterController characterController;
    public string FmodFootStep;
    public float distanceStepSound = 0.5f;

    private Vector3 lastPos;
    private float distMoved;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
        {
            if (characterController.isGrounded)
            {
                distMoved += (lastPos - transform.position).magnitude;
                lastPos = transform.position;
                if (distMoved > distanceStepSound)
                {
                    Debug.Log("je marche ");
                
                    FMODUnity.RuntimeManager.PlayOneShot(FmodFootStep);
                    distMoved = 0.0f;
                }
            }
        }
    }
}
