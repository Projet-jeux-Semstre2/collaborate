﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    public bool canBeActive;
    public bool closeDoor;

    public TextMeshProUGUI timer;
    public GameObject timeUi;
    public Image timeCircle;

    public float t_beforeshutDown = 5;
    private float t_exit;
    [SerializeField] private GameObject _player;
    private Player _playerCs;
    public float distanceActive = 5f;

    public float pourcentageHealth;

    public float yPosUpgrade;

    public CompasManager CompasManager;
    public PointMarker PointMarker;
    public bool havePointMarker;

    private SpawningBaliseIsOn _openSpawnOnBalise;

    //SONS
    public string fmodMusicCapture;
    public string fmodTshutdown;
    public string fmodRenterthezone;
    public string fmodisCature;
    public string fmodIsShutdown;
    private bool JingleWin;
    [SerializeField] private GameObject _AmeliorationManager;
    public GameObject UpgradeCollectible;

    public Material[] canCapture;
    public MeshRenderer coeur;
    public Animator Animator;
    
    
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _openSpawnOnBalise = GetComponent<SpawningBaliseIsOn>();
        _AmeliorationManager = GameObject.Find("AmeliorationManager");
        JingleWin = true;
        _meshRenderer = GetComponent<MeshRenderer>();
        
        
        t = timeObjectif;
        timer.enabled = false;
        timeUi.SetActive(false);
        zone.SetActive(false);
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerCs = _player.GetComponent<Player>();
        
        CompasManager = _player.GetComponentInChildren<CompasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _openSpawnOnBalise.baliseOn = isOn; // savoir si les spawneur vont s'activer.
        _openSpawnOnBalise.isCpatureOn = isCapture;
        if (isOn)
        {
            
            _meshRenderer.material = materials[1];
            if (!isCapture)
            {
                timer.text = ""+ (int)t;
                timer.enabled = true;
                timeCircle.fillAmount = 1 - (t/timeObjectif);
                timeUi.SetActive(true);
                zone.SetActive(true);
            }
            
        }
        else
        {
            _meshRenderer.material = materials[0];
            zone.SetActive(false);
            
            if (Vector3.Distance(transform.position, _player.transform.position) <= distanceActive && canBeActive)
            {
                _playerCs.useButtonOn = true;
                if (Input.GetButtonDown("Use"))
                {
                    
                    FMODUnity.RuntimeManager.PlayOneShot(fmodMusicCapture);
                    isOn = true;
                    _playerCs.useButtonOn = false;
                }
            }
            if(_playerCs.useButtonOn && Vector3.Distance(transform.position, _player.transform.position) > distanceActive && Vector3.Distance(transform.position, _player.transform.position) <= distanceActive * 2 && canBeActive)
            {
                _playerCs.useButtonOn = false;
                
            }


            if (canBeActive && !havePointMarker)
            {
                CompasManager.AddPointMarker(GetComponent<PointMarker>());
                havePointMarker = true;
            }

            if (!canBeActive)
            {
                coeur.material = canCapture[0];
            }
            else
            {
                coeur.material = canCapture[1];
            }
        }

        
        

        if (onCapture && isOn)
        {
            t -= Time.deltaTime;
            timer.text = ""+(int)t;
            timeCircle.fillAmount = 1 - (t/timeObjectif);
            timer.enabled = true;
            timeUi.SetActive(true);
            
            

            if (t <= 0)
            {
                
                t = timeObjectif;
                onCapture = false;
                isCapture = true;
                GameObject inst = Instantiate(UpgradeCollectible, new Vector3(transform.position.x, transform.position.y + yPosUpgrade, transform.position.z), Quaternion.identity);
                inst.GetComponent<UpgradeCollectible>()._ameliorationManager = _AmeliorationManager;
                

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
            timeUi.SetActive(false);
            isOn = true;
            onCapture = false;
            Animator.SetTrigger("isCapture");
            
            
            zone.SetActive(false);
            print("objectif capturé");
            GameObject[] entities = GameObject.FindGameObjectsWithTag("ennemis");
            foreach (GameObject Entities in entities)
            {
                Entities.GetComponent<Entities_Stats>().horlogeFinish = true;
            }
            
            

            _player.GetComponent<Player_Health>().health += (100 - _player.GetComponent<Player_Health>().health) * (pourcentageHealth/100);
            enabled = false;
            
            if (JingleWin) // pour le sons c'est un OS, jingle de win
            {
                Debug.Log("jingle win actif )= ");
                FMODUnity.RuntimeManager.PlayOneShot(fmodisCature);
                JingleWin = false;
            }
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") && isOn) /// ici le joueur capture 
        {
            /// son 
            FMODUnity.RuntimeManager.PlayOneShot(fmodRenterthezone);
            
            onCapture = true;
        }
    }

    

    private void OnTriggerExit(Collider other) // ici le joueur sort de la capture
    {
        if (other.CompareTag("Player")&& onCapture)
        {
            /// son 
            FMODUnity.RuntimeManager.PlayOneShot(fmodTshutdown); 
            
            onCapture = false;
            t_exit = 0;
        }
    }

    void shutDownZone() //// fonction qui reboot la balise si il ya echec de capture
    {
        /// son 
        FMODUnity.RuntimeManager.PlayOneShot(fmodIsShutdown);
        
        t = timeObjectif;
        timer.enabled = false;
        timeUi.SetActive(false);
        onCapture = false;
        isOn = false;
        t_exit = 0;
    }

    void ExitOnCpature() /// méthode qui compte le temps si il ya echec de la capture
    {
        t_exit += Time.deltaTime;
        if (t_exit >= t_beforeshutDown)
        {
            shutDownZone();
        }
    }
    
}
