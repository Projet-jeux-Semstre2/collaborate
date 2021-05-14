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
    private string typeArmeAggresif = "ArmeForAggresif";
    private string typeArmeVif = "ArmeForVif";
    private string typeArmeCreateur = "ArmeForCreateur";

    public ColorGrading colorGrading;
    public PostProcessVolume volume;
    public Color agressif_Color;
    public Color tank_Color;
    public Color vif_color;
    public Color createur_Color;
    
    public float switchTime;
    private float t_switch;

    private void OnEnable()
    {
        typeArmeActive = typeArmeTank;
    }

    private void Start()
    {
        volume.profile.TryGetSettings(out colorGrading);
    }

    private void Update()
    {
        changeFocus();
        _shotGunManager.typeArmeActive = typeArmeActive;
    }

    public void changeFocus()
    {
        t_switch += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A) && t_switch >= switchTime)
        {
            switch (typeArmeActive)
            {
                case "ArmeForAgressif":
                    typeArmeActive = typeArmeTank;
                    colorGrading.colorFilter.value = tank_Color;
                    break;
                case "ArmeForTank":
                    typeArmeActive = typeArmeCreateur;
                    colorGrading.colorFilter.value = createur_Color;
                    break;
                case "ArmeForCreateur":
                    typeArmeActive = typeArmeVif;
                    colorGrading.colorFilter.value = vif_color;
                    break;
                case "ArmeForVif":
                    typeArmeActive = typeArmeAggresif;
                    colorGrading.colorFilter.value = agressif_Color;
                    break;
            }

            t_switch = 0;
        }


        
    }

}
