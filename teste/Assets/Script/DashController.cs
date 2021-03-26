using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public bool isDashing;

    [SerializeField] private int dashAttempts = 0;
    private float dashStartTime = 0;
    public float coolDown;
    private bool canDash = true;

    
    public float dashSpeed = 30f;

    private Player _playerController;
    private CharacterController _characterController;

    [SerializeField] private ParticleSystem forwardDashParticleSystem;
    [SerializeField] private ParticleSystem backwardDashParticleSystem;
    [SerializeField] private ParticleSystem rightDashParticleSystem;
    [SerializeField] private ParticleSystem leftDashParticleSystem;
    
    
    private void Start()
    {
        _playerController = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleDash();
    }

    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.LeftAlt);
        if (isTryingToDash && !isDashing)
        {
            if (dashAttempts <= Mathf.Infinity && canDash) //si on veut mettre un nb max de dash possible remplacer le infini par un nb
            {
                StartCoroutine(OnStartDash());
            }
        }

        if (isDashing)
        {
            if (Time.time - dashStartTime <= 0.4f)
            {
                if (_playerController.moveDirection.x == 0 && _playerController.moveDirection.z == 0)
                {
                    //just dash forward
                    _characterController.Move(transform.forward * dashSpeed  * Time.deltaTime);

                }
                else
                {
                    Vector3 vectornrmz = new Vector3(_playerController.moveDirection.x, 0, _playerController.moveDirection.z);
                    _characterController.Move(vectornrmz.normalized * dashSpeed  * Time.deltaTime);
                }
            }
            else
            {
                OnEndDash();
            }
        }
    }

    IEnumerator OnStartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
        dashAttempts += 1;
        StartCoroutine(CoolDown());
        PlayDashParticles();
        yield return null;
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
    }

    void PlayDashParticles()
    {
        Vector3 inputVector = _playerController.inputVector;

        if (inputVector.z > 0 && Mathf.Abs(inputVector.x) <= inputVector.z)
        {
            forwardDashParticleSystem.gameObject.SetActive(true);
            forwardDashParticleSystem.Play();
            return;
        }

        if (inputVector.z < 0 && Mathf.Abs(inputVector.x) <= Mathf.Abs(inputVector.z))
        {
            backwardDashParticleSystem.gameObject.SetActive(true);
            backwardDashParticleSystem.Play();
            return;
        }

        if (inputVector.x > 0)
        {
            rightDashParticleSystem.gameObject.SetActive(true);
            rightDashParticleSystem.Play();
            return;
        }

        if (inputVector.x < 0)
        {
            leftDashParticleSystem.gameObject.SetActive(true);
            leftDashParticleSystem.Play();
            return;
        }
        
        forwardDashParticleSystem.gameObject.SetActive(true);
        forwardDashParticleSystem.Play();
        
    }

    IEnumerator CoolDown()
    {
        canDash = false;
        
        yield return new WaitForSeconds(coolDown);

        canDash = true;
    }
}
