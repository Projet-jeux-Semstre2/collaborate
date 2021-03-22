using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balise_Manager : MonoBehaviour
{

    public Material[] materials;

    public bool isOn;
    private MeshRenderer _meshRenderer;

    public GameObject zone;

    private float t;
    public float timeObjectif = 10f;

    private bool onCapture;
    public bool isCapture;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            _meshRenderer.material = materials[1];
            if (!isCapture)
            {
                zone.SetActive(true);
            }
            
        }
        else
        {
            _meshRenderer.material = materials[0];
            zone.SetActive(false);
        }


        if (onCapture)
        {
            t += Time.deltaTime;

            if (t >= timeObjectif)
            {
                t = 0;
                onCapture = false;
                isCapture = true;
            }
        }

        if (isCapture)
        {
            isOn = true;
            onCapture = false;
            t = 0;
            zone.SetActive(false);
            print("objectif capturé");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")&& !isOn)
        {
            isOn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onCapture = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && onCapture)
        {
            t = 0;
            isOn = false;
            onCapture = false;
            
        }
    }
}
