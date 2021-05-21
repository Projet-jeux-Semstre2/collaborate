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
    
    
    
    private void Start()
    {
        typeArmeActive = typeArmeTank;
        volume.profile.TryGetSettings(out _colorGrading);
        _colorGrading.colorFilter.value = tank_Color;
        StartCoroutine(SwitchFilter());
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
    }

    private void Update()
    {
        changeFocus();
        _shotGunManager.typeArmeActive = typeArmeActive;

        
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
            StartCoroutine(SwitchFilter());
            t_switch = 0;
            switch (armeID)
            {
                case 0:
                    typeArmeActive = typeArmeTank;
                    _colorGrading.colorFilter.value = tank_Color;
                    
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
                    
                    break;
                case 1:
                    typeArmeActive = typeArmeCreateur;
                    _colorGrading.colorFilter.value = createur_Color;
                    
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
                    
                    break;
                case 2:
                    typeArmeActive = typeArmeVif;
                    _colorGrading.colorFilter.value = vif_color;
                    
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
                    
                    break;
                case 3:
                    typeArmeActive = typeArmeAgressif;
                    _colorGrading.colorFilter.value = agressif_Color;
                    
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
                    
                    break;
            }
        }
        
        
        
        
        
    }


    IEnumerator SwitchFilter()
    {
        lerp = false;
        
        switch (typeArmeActive)
        {
            case "ArmeForAgressif":
                _colorGrading.colorFilter.value = tank_Color; 
                break;
            case "ArmeForTank": 
                _colorGrading.colorFilter.value = createur_Color; 
                break; 
            case "ArmeForCreateur": 
                _colorGrading.colorFilter.value = vif_color; 
                break;
            case "ArmeForVif": 
                _colorGrading.colorFilter.value = agressif_Color; 
                break;
        }
        
        yield return new WaitForSeconds(timeEffect);

        lerp = true;
        while (_colorGrading.colorFilter.value != Color.white && lerp)
        {
            _colorGrading.colorFilter.value = Color.Lerp(_colorGrading.colorFilter.value, Color.white, Time.deltaTime * lerpSpeed);
            
            yield return null;
        }

        yield return null;

    }

}
