using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glock_Manager : MonoBehaviour
{
    public weaponGlock weaponGlock;
    public GameObject ameliorationManager;
    private Player _player;
    
    [Header("Amélioration")] 
    public float niveauJauge;
    [Tooltip("Palier actuel")]
    public int palierJauge;
    [Tooltip("Chaque float correspond à l'espacement en niveau entre les paliers (0 = niveau avant palier 1, etc)")]
    public float[] niveauBetweenPalier;
    public RectTransform barreNiveau;



    private bool _palier0Finish, _palier1Finish, _palier2Finish, _palier3Finish, _palier4Finish;

    public float[] penetrationStats, damageStats, explosifBulletStats, fireRateStats;
    public float rafaleLength, timeBetweenShoot;
    public bool canRafal;

    public Text unlockText;


    private void OnEnable()
    {
        ameliorationManager = GameObject.Find("AmeliorationManager");
        //ameliorationManager.GetComponent<AmeliorationManager>()._weaponGlock = weaponGlock;
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
        }*/
    }
    
    
    void Palier0()
    {
        //tout lvl 0 (stats de base)
        weaponGlock.Degats = damageStats[0];
        weaponGlock.radiusExplosion = explosifBulletStats[0];
        weaponGlock.penetrationNumber = penetrationStats[0];
        weaponGlock.fireRate = fireRateStats[0];
        _palier0Finish = true;
    }

    /*void Palier1()
    {
        weaponGlock.penetrationNumber = penetrationStats[1];
        _palier1Finish = true;
    }
    
    void Palier2()
    {
        weaponGlock.penetrationNumber = penetrationStats[2];
        _palier2Finish = true;
    }
    
    void Palier3()
    {
        canRafal = true;
        weaponGlock.fireRate = fireRateStats[1];
        weaponGlock.Degats = damageStats[1];
        StartCoroutine(UnlockCompétenceTxt(unlockText, "Tire en rafale débloqué"));
        _palier3Finish = true;
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
