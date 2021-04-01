using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balise_Fonctionnement : MonoBehaviour
{

    public Material[] materials;

    public bool isOn;
    private MeshRenderer _meshRenderer;

    public GameObject zone;
    public bool isCount;

    [SerializeField] private float t;
    public float timeObjectif = 10f;

    public bool onCapture;
    public bool isCapture;

    public Text timer;

    public float t_beforeshutDown = 5;
    private float t_exit;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        t = timeObjectif;
        timer.enabled = false;
        zone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            
            _meshRenderer.material = materials[1];
            if (!isCapture)
            {
                timer.text = "Temps de capture : " + t;
                timer.enabled = true;
                zone.SetActive(true);
            }
            
        }
        else
        {
            _meshRenderer.material = materials[0];
            zone.SetActive(false);
        }

        
        

        if (onCapture && isOn)
        {
            t -= Time.deltaTime;
            timer.text = "Temps de capture : " + t;
            
            timer.enabled = true;
            
            

            if (t <= 0)
            {
                t = timeObjectif;
                onCapture = false;
                isCapture = true;
            }
        }

        if (!onCapture && isOn)
        {
            ExitOnCpature();
        }
        
        
        
        if(isCapture)
        {
            t = timeObjectif;
            timer.enabled = false;
            isOn = true;
            onCapture = false;
            
            zone.SetActive(false);
            print("objectif capturé");
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")&& !isOn)
        {
            isOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOn)
        {
            onCapture = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& onCapture)
        {
            onCapture = false;
            t_exit = 0;
        }
    }

    void shutDownZone()
    {
        
        t = timeObjectif;
        timer.enabled = false;
        onCapture = false;
        isOn = false;
        t_exit = 0;
    }

    void ExitOnCpature()
    {
        t_exit += Time.deltaTime;
        if (t_exit >= t_beforeshutDown)
        {
            shutDownZone();
        }
    }
}
