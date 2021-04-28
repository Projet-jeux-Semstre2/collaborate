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

    public bool canExplosePlayer;


    public float[] rangeStats, damageStats, surchauffeStats, explosionStats, explosionRadiusStats, jumpForceStats, switchTimeStats;
    public float[] sprayStats, relaodSpeedStats, surchauffeLanceGreStats, explosionForceLanceGreStats, radiusExplosionGre;
    public int[] speedManiementStats, nbBulletStats;

    [Space(25)]
    public bool lanceGrenadeUnlock;
    public Text unlockText;
    
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
        Palier0();
    }

    private void Update()
    {
        
        /*barreNiveau.sizeDelta = Vector2.Lerp(barreNiveau.sizeDelta, new Vector2(barreNiveau.sizeDelta.x, (310/niveauBetweenPalier[niveauBetweenPalier.Length -1]) * niveauJauge), Time.time);
        
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
        }*/
        
        
        
        
        
        
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
        weaponPompe.explosionRadius = explosionRadiusStats[0];
        weaponPompe.bulletExplosionForce = jumpForceStats[0];
        weaponPompe.surchauffeAddLanceGrenade = surchauffeLanceGreStats[0];
        weaponPompe.reloadSpeed = relaodSpeedStats[0];
        weaponPompe.sprayX = sprayStats[0]; // a voir 
        weaponPompe.tromblonNombre = nbBulletStats[0]; // a voir

        weaponPompe.grenade.GetComponent<Grenade_Explosion>().force = explosionForceLanceGreStats[0];
        weaponPompe.grenade.GetComponent<Grenade_Explosion>().radius += radiusExplosionGre[0];
        weaponPompe.grenade.GetComponent<Grenade_Explosion>().exlosionRadius += radiusExplosionGre[0];
        _palier0Finish = true;
    }

    /*void Palier1()
    {
        //range lvl 1 & surchauffe lvl 1
        weaponPompe.maxRange = rangeStats[1];
        surchauffe = surchauffeStats[1];
        weaponPompe.sprayX = sprayStats[1]; // a voir
        _palier1Finish = true;
    }
    
    void Palier2()
    {
        //damage lvl 1 & speedManiement lvl 1
        weaponPompe.Degats = damageStats[1];
        weaponPompe.vitesseSrint -= speedManiementStats[1];
        weaponPompe.vitessWalk -= speedManiementStats[1];
        weaponPompe.tromblonNombre = nbBulletStats[1]; // a voir
        _palier2Finish = true;
    }
    
    void Palier3()
    {
        //explosion lvl 1 + affect le joueur & surchauffe lvl 2
        weaponPompe.explosionForce = explosionStats[1];
        StartCoroutine(UnlockCompétenceTxt(unlockText, "Tire sous tes pieds pour sauter"));
        canExplosePlayer = true;
        weaponPompe.explosionForce = jumpForceStats[1];
        surchauffe = surchauffeStats[2];
        _palier3Finish = true;
    }
    
    void Palier4()
    {
        lanceGrenadeUnlock = true;
        weaponPompe.sprayX = sprayStats[2];
        StartCoroutine(UnlockCompétenceTxt(unlockText, "Lance-Grenade débloqué, bouton droit de la souris pour l'utiliser"));
        _palier4Finish = true;
    }
    
    void Palier5( )
    {
        //explosion lvl 2 && damage lvl 2
        weaponPompe.Degats = damageStats[2];
        weaponPompe.explosionForce = explosionStats[2];
        weaponPompe.explosionForce = jumpForceStats[2];
        weaponPompe.tromblonNombre = nbBulletStats[2]; //a voir
        _palier5Finish = true;
    }
    
    void Palier6()
    {
        //range lvl 2 & speedManiement lvl 2 & unlock un nouveau tire
        weaponPompe.maxRange = rangeStats[2];
        weaponPompe.vitesseSrint -= speedManiementStats[2];
        weaponPompe.vitessWalk -= speedManiementStats[2];
        weaponPompe.tromblonNombre = nbBulletStats[3]; //a voir
        _palier6Finish = true;
    }*/


    public IEnumerator UnlockCompétenceTxt(Text objectText, string phrase)
    {
        objectText.gameObject.SetActive(true);
        objectText.text = phrase;
        yield return new WaitForSeconds(0.8f);

        Color colorwithoutA = objectText.color;
        colorwithoutA.a = 0;
        while (objectText.color != colorwithoutA)
        {
            Color lerp = Color.Lerp(objectText.color, colorwithoutA, Time.deltaTime * 2.5f);
            objectText.color = lerp;
            yield return null;
        }
        
        objectText.gameObject.SetActive(false);
    }
    
    
    
    
    

    
}
