using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
    
    
    public string typeArmeActive;

    private GameObject UpgradeManager;
    


    
        
        
    private void Start()
    {
        weaponPompe = GetComponentInChildren<weaponPompe>();
        _player = GetComponent<Player>();
        
        Palier0();
        
        UpgradeManager = GameObject.Find("AmeliorationManager");
    }

    private void Update()
    {

        SurchauffeManager();
        ChangeViseur();
        
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

    private void ChangeViseur()
    {
        if (!UpgradeManager)
        {
            switch (typeArmeActive)
            {
                case "ArmeForAgressif":
                    weaponPompe.viseur[0].SetActive(false);
                    weaponPompe.viseur[1].SetActive(true);
                    weaponPompe.viseur[2].SetActive(false);
                    weaponPompe.viseur[3].SetActive(false);
                    weaponPompe.viseurActive = weaponPompe.viseur[1];
                    break;
                            
                case "ArmeForTank":
                    weaponPompe.viseur[0].SetActive(true);
                    weaponPompe.viseur[1].SetActive(false);
                    weaponPompe.viseur[2].SetActive(false);
                    weaponPompe.viseur[3].SetActive(false);
                    weaponPompe.viseurActive = weaponPompe.viseur[0];
                    break;
                            
                case "ArmeForCreateur":
                    weaponPompe.viseur[0].SetActive(false);
                    weaponPompe.viseur[1].SetActive(false);
                    weaponPompe.viseur[2].SetActive(false);
                    weaponPompe.viseur[3].SetActive(true);   
                    weaponPompe.viseurActive = weaponPompe.viseur[3];
                    break;
                            
                case "ArmeForVif":
                    weaponPompe.viseur[0].SetActive(false);
                    weaponPompe.viseur[1].SetActive(false);
                    weaponPompe.viseur[2].SetActive(true);
                    weaponPompe.viseur[3].SetActive(false);
                    weaponPompe.viseurActive = weaponPompe.viseur[2];
                    break;
            }
        }
        else
        {
            weaponPompe.viseur[0].SetActive(false);
            weaponPompe.viseur[1].SetActive(false);
            weaponPompe.viseur[2].SetActive(false);
            weaponPompe.viseur[3].SetActive(false);
        }
        
    }


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
