using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShotGun_Manager : MonoBehaviour
{
    public weaponPompe weaponPompe;
    private Player _player;
    
    [Header("Surchauffe")]
    public float surchauffe;
    public float niveauSurchauffe;
    public float t_forLooseSurchauffe;
    public RectTransform surchauffeImage;


    [Header("Amélioration")] 
    public float niveauJauge;
    [Tooltip("Palier actuel")]
    public int palierJauge;
    [Tooltip("Chaque float correspond à l'espacement en niveau entre les paliers (0 = niveau avant palier 1, etc)")]
    public float[] niveauBetweenPalier;
    public RectTransform barreNiveau;

    private bool _palier0Finish, _palier1Finish,_palier2Finish,_palier3Finish,_palier4Finish,_palier5Finish,_palier6Finish;
    
    
    
    public float[] rangeStats, damageStats, surchauffeStats, explosionStats;
    public int[] speedManiementStats;

    [Space(25)]
    public bool lanceGrenadeUnlock;
    public GameObject textLanceGrenadeUnlock;
    
    [Header("Sons")]
    // LE SONS
    public string FmodSurchauffe;
    public string FmodCooling;

    private float t;
    private bool isSurchauffeMax = false;

    private void Start()
    {
        weaponPompe = GetComponentInChildren<weaponPompe>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        
        barreNiveau.sizeDelta = Vector2.Lerp(barreNiveau.sizeDelta, new Vector2(barreNiveau.sizeDelta.x, (310/niveauBetweenPalier[niveauBetweenPalier.Length -1]) * niveauJauge), Time.time);
        
        if (palierJauge < niveauBetweenPalier.Length && niveauJauge >= niveauBetweenPalier[palierJauge] )
        {
            palierJauge++;
        }

        switch (palierJauge)
        {
            case 0:
                if (!_palier0Finish)
                {
                    Palier0();
                }
                
                break;
            case 1:
                if (!_palier1Finish)
                {
                    Palier1();
                }

                break;
            case 2:
                if (!_palier2Finish)
                {
                    Palier2();
                }

                break;
            case 3:
                if (!_palier3Finish)
                {
                    Palier3();
                }

                break;
            case 4:
                if (!_palier4Finish)
                {
                    Palier4();
                }

                break;
            case 5:
                if (!_palier5Finish)
                {
                    Palier5();
                }

                break;
            case 6:
                if (!_palier6Finish)
                {
                    Palier6();
                }

                break;
        }
        
        
        
        
        
        
        SurchauffeManager();
    }
    
    
    
    
    
    void SurchauffeMax() // quand on atteint la surchauffe max
    {
        FMODUnity.RuntimeManager.PlayOneShot(FmodSurchauffe, transform.position);
        isSurchauffeMax = true;
    }


    void SurchauffeManager()
    {
        float add = 1115 / surchauffe;
        surchauffeImage.sizeDelta = Vector2.Lerp(surchauffeImage.sizeDelta, new Vector2(add * niveauSurchauffe, surchauffeImage.sizeDelta.y), Time.time);
        
        
        t += Time.deltaTime;
        if (niveauSurchauffe > 0 && t >= t_forLooseSurchauffe && !weaponPompe.Reloading)
        {
            FMODUnity.RuntimeManager.PlayOneShot(FmodCooling, transform.position);
            niveauSurchauffe--;
            t = 0;
        }

        if (niveauSurchauffe >= surchauffe) // lance le sons de surchauffe max une seule foix
        {
            if (!isSurchauffeMax)
            {
                SurchauffeMax();
            }
        }
    }


    void Palier0()
    {
        //tout lvl 0 (stats de base)
        weaponPompe.maxRange = rangeStats[0];
        weaponPompe.Degats = damageStats[0];
        surchauffe = surchauffeStats[0];
        weaponPompe.vitesseSrint -= speedManiementStats[0];
        weaponPompe.vitessWalk -= speedManiementStats[0];
        weaponPompe.explosionForce = explosionStats[0];
        _palier0Finish = true;
    }

    void Palier1()
    {
        //range lvl 1 & surchauffe lvl 1
        weaponPompe.maxRange = rangeStats[1];
        surchauffe = surchauffeStats[1];
        _palier1Finish = true;
    }
    
    void Palier2()
    {
        //damage lvl 1 & speedManiement lvl 1
        weaponPompe.Degats = damageStats[1];
        weaponPompe.vitesseSrint -= speedManiementStats[1];
        weaponPompe.vitessWalk -= speedManiementStats[1];
        _palier2Finish = true;
    }
    
    void Palier3()
    {
        //explosion lvl 1 + affect le joueur & surchauffe lvl 2
        weaponPompe.explosionForce = explosionStats[1];
        surchauffe = surchauffeStats[2];
        _palier3Finish = true;
    }
    
    void Palier4()
    {
        lanceGrenadeUnlock = true;
        StartCoroutine(UnlockCompétenceTxt(textLanceGrenadeUnlock));
        _palier4Finish = true;
    }
    
    void Palier5( )
    {
        //explosion lvl 2 && damage lvl 2
        weaponPompe.Degats = damageStats[2];
        weaponPompe.explosionForce = explosionStats[2];
        _palier5Finish = true;
    }
    
    void Palier6()
    {
        //range lvl 2 & speedManiement lvl 2 & unlock un nouveau tire
        weaponPompe.maxRange = rangeStats[2];
        weaponPompe.vitesseSrint -= speedManiementStats[2];
        weaponPompe.vitessWalk -= speedManiementStats[2];
        _palier6Finish = true;
    }


    IEnumerator UnlockCompétenceTxt(GameObject objectText)
    {
        objectText.SetActive(true);
        yield return new WaitForSeconds(0.8f);

        Color colorwithoutA = objectText.GetComponent<Text>().color;
        colorwithoutA.a = 0;
        while (objectText.GetComponent<Text>().color != colorwithoutA)
        {
            Color lerp = Color.Lerp(objectText.GetComponent<Text>().color, colorwithoutA, Time.deltaTime * 2.5f);
            objectText.GetComponent<Text>().color = lerp;
            yield return null;
        }
        
        objectText.SetActive(false);
    }
    
    
    
    
    

    
}
