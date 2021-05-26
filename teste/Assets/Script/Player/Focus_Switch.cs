using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Focus_Switch : MonoBehaviour
{
    public ShotGun_Manager _shotGunManager;


    [Header("Type d'arme")] 
    private string typeArmeActive;
    private string typeArmeTank = "ArmeForTank";
    private string typeArmeAgressif = "ArmeForAgressif";
    private string typeArmeVif = "ArmeForVif";
    private string typeArmeCreateur = "ArmeForCreateur";
    public int armeID;

    private ColorGrading _colorGrading;
    public PostProcessVolume volume;
    public Color agressif_Color;
    public Color tank_Color;
    public Color vif_color;
    public Color createur_Color;
    
    [Header("CoolDown")]
    public float switchTime;
    private float t_switch;

    [Header("SwitchEffect")] 
    public float timeEffect;
    public float lerpSpeed;
    public bool lerp;
    public GameObject[] focusUi;
    public GameObject focusUiParent;
    public float rotateUi;
    
    // sons
    public string FmodSwitch;
    
    [Header("Sur le ShotGun")]
    public Color[] emissiveShotGun;
    public MeshRenderer[] shotGunMeshRenderer;
    public MeshRenderer[] OverLoadRenderers;



    private void Start()
    {
        typeArmeActive = typeArmeTank;
        volume.profile.TryGetSettings(out _colorGrading);
        armeID = 0;
        foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
        {
            Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
            if (entitiesManager.type == "Tank")
            {
                entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
            }
            else
            {
                entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
            }
        }

        foreach (var renderer in shotGunMeshRenderer)
        {
            renderer.materials[1].SetColor("_EmissionColor", emissiveShotGun[0]);
            renderer.materials[1].color = emissiveShotGun[0];
        }

        foreach (var renderer in OverLoadRenderers)
        {
            renderer.material.SetColor("_EmissionColor", emissiveShotGun[0]);
            renderer.material.color = emissiveShotGun[0];
        }
        
        
    }

    private void Update()
    {
        changeFocus();
        _shotGunManager.typeArmeActive = typeArmeActive;
        
        
        focusUiParent.transform.Rotate(0,0, rotateUi * Time.deltaTime);

        
    }
    
    

    public void changeFocus()
    {
        t_switch += Time.deltaTime;
        t_switch = Mathf.Clamp(t_switch, 0, switchTime);

        armeID = Mathf.Clamp(armeID, 0, 3);
        
        if (Input.mouseScrollDelta.y > 0 && t_switch >= switchTime)
        {
            armeID++;

            if (armeID > 3)
            {
                armeID = 0;
            }
            
            
        }
        if (Input.mouseScrollDelta.y < 0 && t_switch >= switchTime)
        {
            armeID--;

            if (armeID <0)
            {
                armeID = 3;
            }
            
        }

        if (Input.mouseScrollDelta.y > 0 && t_switch >= switchTime || Input.mouseScrollDelta.y < 0 && t_switch >= switchTime)
        {
            
            switch (armeID)
            {
                case 0:
                    typeArmeActive = typeArmeTank;

                    foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                    {
                        Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                        if (entitiesManager.type == "Tank")
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                        }
                        else
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                        }
                    }

                    foreach (var image in focusUi)
                    {
                        image.SetActive(false);
                    }
                    
                    focusUi[0].SetActive(true);
                    
                    break;
                case 1:
                    typeArmeActive = typeArmeCreateur;

                    foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                    {
                        Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                        if (entitiesManager.type == "Createur")
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                        }
                        else
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                        }
                    }
                    
                    foreach (var image in focusUi)
                    {
                        image.SetActive(false);
                    }
                    
                    focusUi[1].SetActive(true);
                    
                    break;
                case 2:
                    typeArmeActive = typeArmeVif;

                    foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                    {
                        Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                        if (entitiesManager.type == "Vif")
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                        }
                        else
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                        }
                    }
                    
                    foreach (var image in focusUi)
                    {
                        image.SetActive(false);
                    }
                    
                    focusUi[2].SetActive(true);
                    
                    break;
                case 3:
                    typeArmeActive = typeArmeAgressif;

                    foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                    {
                        Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                        if (entitiesManager.type == "Agressif")
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                        }
                        else
                        {
                            entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                        }
                    }
                    
                    foreach (var image in focusUi)
                    {
                        image.SetActive(false);
                    }
                    
                    focusUi[3].SetActive(true);
                    
                    break;
            }

            t_switch = 0;

            StartCoroutine(SwitchFilter());
            changeShotGunColor();
        }

        if (t_switch >= switchTime)
        {
            if (Input.GetKeyDown("1") && typeArmeActive != typeArmeTank)
            {
                typeArmeActive = typeArmeTank;

                foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                {
                    Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                    if (entitiesManager.type == "Tank")
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                    }
                    else
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                    }
                }
                StartCoroutine(SwitchFilter());
                t_switch = 0;
            }
            
            if (Input.GetKeyDown("2") && typeArmeActive != typeArmeCreateur)
            {
                typeArmeActive = typeArmeCreateur;

                foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                {
                    Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                    if (entitiesManager.type == "Createur")
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                    }
                    else
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                    }
                }
                StartCoroutine(SwitchFilter());
                t_switch = 0;
            }
            
            if (Input.GetKeyDown("3") && typeArmeActive != typeArmeVif)
            {
                typeArmeActive = typeArmeVif;

                foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                {
                    Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                    if (entitiesManager.type == "Vif")
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                    }
                    else
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                    }
                }
                StartCoroutine(SwitchFilter());
                t_switch = 0;
            }
            
            if (Input.GetKeyDown("4") && typeArmeActive != typeArmeAgressif)
            {
                typeArmeActive = typeArmeAgressif;
                
                    
                foreach (GameObject entities in GameObject.FindGameObjectsWithTag("ennemis"))
                {
                    Entities_Manager entitiesManager = entities.GetComponent<Entities_Manager>();
                    if (entitiesManager.type == "Agressif")
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[1];
                    }
                    else
                    {
                        entitiesManager.renderer.material = entitiesManager.ChangeMaterials[0];
                    }
                }
                StartCoroutine(SwitchFilter());
                t_switch = 0;
            }
        }
        
        
        
    }


    IEnumerator SwitchFilter()
    {
        lerp = false;
        
        switch (typeArmeActive)
        {
            case "ArmeForTank":
                _colorGrading.colorFilter.value = tank_Color; 
                break;
            case "ArmeForCreateur": 
                _colorGrading.colorFilter.value = createur_Color; 
                break; 
            case "ArmeForVif": 
                _colorGrading.colorFilter.value = vif_color; 
                break;
            case "ArmeForAgressif": 
                _colorGrading.colorFilter.value = agressif_Color; 
                break;
        }
        //sons
        bool canOneShot = true;
        if (canOneShot)
        {
                        FMODUnity.RuntimeManager.PlayOneShot(FmodSwitch);
        }
        
        yield return new WaitForSeconds(timeEffect);
        canOneShot = false;

        lerp = true;
        while (_colorGrading.colorFilter.value != Color.white && lerp)
        {
            
            _colorGrading.colorFilter.value = Color.Lerp(_colorGrading.colorFilter.value, Color.white, Time.deltaTime * lerpSpeed);
            
            yield return null;
        }

        yield return null;

    }
    
    void changeShotGunColor()
    {
        foreach (var renderer in shotGunMeshRenderer)
        {
            switch (armeID)
            {
                case 0:
                    renderer.materials[1].SetColor("_EmissionColor", emissiveShotGun[0]);
                    renderer.materials[1].color = emissiveShotGun[0];
                    break;
                case 1:
                    renderer.materials[1].SetColor("_EmissionColor", emissiveShotGun[1]);
                    renderer.materials[1].color = emissiveShotGun[1];
                    break;
                case 2:
                    renderer.materials[1].SetColor("_EmissionColor", emissiveShotGun[2]);
                    renderer.materials[1].color = emissiveShotGun[2];
                    break;
                case 3:
                    renderer.materials[1].SetColor("_EmissionColor", emissiveShotGun[3]);
                    renderer.materials[1].color = emissiveShotGun[3];
                    break;
            }
            
        }
        
        foreach (var renderer in OverLoadRenderers)
        {
            switch (armeID)
            {
                case 0:
                    renderer.material.SetColor("_EmissionColor", emissiveShotGun[0]);
                    renderer.material.color = emissiveShotGun[0];
                    break;
                case 1:
                    renderer.material.SetColor("_EmissionColor", emissiveShotGun[1]);
                    renderer.material.color = emissiveShotGun[1];
                    break;
                case 2:
                    renderer.material.SetColor("_EmissionColor", emissiveShotGun[2]);
                    renderer.material.color = emissiveShotGun[2];
                    break;
                case 3:
                    renderer.material.SetColor("_EmissionColor", emissiveShotGun[3]);
                    renderer.material.color = emissiveShotGun[3];
                    break;
            }
            
        }
    }

}
