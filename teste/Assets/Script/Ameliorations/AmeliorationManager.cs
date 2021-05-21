using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AmeliorationManager : MonoBehaviour
{

    private ShotGun_Manager _shotGunManager;
    private weaponPompe _weaponPompe;

    public Transform[] placementButton;
    public GameObject[] upgradeButtons;
    public GameObject[] buttonsChoose;
    
    
    
    public GameObject range, surcharge, speedmaniement, explosiveBullet, lanceGre;

    public GameObject UpgradeMenu;

    private void Start()
    {
        _shotGunManager = GameObject.FindWithTag("Player").GetComponent<ShotGun_Manager>();
        _weaponPompe = GameObject.FindWithTag("Player").GetComponentInChildren<weaponPompe>();
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpgradeMenu.GetComponent<UpgradeAnimation>().myAnimator.SetTrigger("Open");
        foreach (var button in buttonsChoose)
        {
            if (button)
            {
                button.SetActive(false);
            }
        }
        foreach (var placement in placementButton)
        {
            if (placement.gameObject)
            {
                placement.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (UpgradeMenu.GetComponent<UpgradeAnimation>().close)
        {
            foreach (var button in buttonsChoose)
            {
                if (button)
                {
                    button.SetActive(false);
                }
            }

            foreach (var placement in placementButton)
            {
                if (placement.gameObject)
                {
                    placement.gameObject.SetActive(false);
                }
            }
            PauseMenu.canLock = true;
            PauseMenu.pauseTime = false;
        }
    }


    public void Desac()
    {
        UpgradeMenu.GetComponent<UpgradeAnimation>().myAnimator.SetTrigger("Close");
    }

    public void playMusic(string zik)
    {
        FMODUnity.RuntimeManager.PlayOneShot(zik);
    }

    




    public IEnumerator chooseUpgradeRandom()
    {
        List<GameObject> temp = new List<GameObject>();
        temp.AddRange(upgradeButtons);
        print(temp);
        
        for (int i = 0; i < buttonsChoose.Length; i++)
        {
            int rd = Random.Range(0, upgradeButtons.Length);

            if (!buttonsChoose.Contains(upgradeButtons[rd]))
            {
                buttonsChoose[i] = upgradeButtons[rd];
                buttonsChoose[i].transform.position = placementButton[i].position;
                buttonsChoose[i].SetActive(true);
            }
            else
            {
                i--;
            }
            
        }

        yield return null;
    }
    
    
    
    //Portée
    public void RangeUpgrade(int UpgradeLevel)
    {
        _weaponPompe.maxRange = _shotGunManager.rangeStats[UpgradeLevel];
        range.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;

    }
    
    //Surcharge
    public void SurchargeUpgrade(int UpgradeLevel)
    {
        switch (UpgradeLevel)
        {
            case 1:
                _shotGunManager.surchauffe = _shotGunManager.surchauffeStats[UpgradeLevel];
                break;
            case 2:
                _weaponPompe.reloadSpeed = _shotGunManager.relaodSpeedStats[1];
                break;
            case 3:
                _weaponPompe.reloadSpeed = _shotGunManager.relaodSpeedStats[2];
                _shotGunManager.surchauffe = _shotGunManager.surchauffeStats[2];
                break;
        }

        surcharge.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;

    }
    
    //SpeedManiement
    public void SpeedManiementUpgarde(int UpgradeLevel)
    {
        switch (UpgradeLevel)
        {
            case 1:
                _weaponPompe.vitesseSrint -=  _shotGunManager.speedManiementStats[1];
                _weaponPompe.vitessWalk -= _shotGunManager.speedManiementStats[1]; 
                break;
            case 2:
                _weaponPompe.timeBetweenChange = _shotGunManager.switchTimeStats[1];
                break;
            case 3: 
                _weaponPompe.timeBetweenChange = _shotGunManager.switchTimeStats[2];
                _weaponPompe.vitesseSrint -=  _shotGunManager.speedManiementStats[2];
                _weaponPompe.vitessWalk -= _shotGunManager.speedManiementStats[2]; 
                break;
        }
        speedmaniement.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;
        
    }
    
    //Explosion
    public void ExplosionUpgarde(int UpgradeLevel)
    {
        switch (UpgradeLevel)
        {
            case 1:
                _weaponPompe.explosionForce = _shotGunManager.explosionStats[1];
                _weaponPompe.explosionRadius = _shotGunManager.explosionRadiusStats[1];
                break;
            case 2:
                _shotGunManager.canExplosePlayer = true;
                _weaponPompe.bulletExplosionForce = _shotGunManager.jumpForceStats[UpgradeLevel];
                _shotGunManager.UnlockCompétenceTxt(_shotGunManager.unlockText, "Rocket Jump débloqué, tire sous tes pieds pour effectuer un saut");
                break;
            case 3:
                _weaponPompe.explosionForce = _shotGunManager.explosionStats[2];
                _weaponPompe.explosionRadius = _shotGunManager.explosionRadiusStats[2];
                break;
        }
        explosiveBullet.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;
    }

    //LanceGrenade
    public void LanceGranadeUpgrade(int UpgradeLevel)
    {
        switch (UpgradeLevel)
        {
            case 1:
                _shotGunManager.lanceGrenadeUnlock = true;
                _shotGunManager.UnlockCompétenceTxt(_shotGunManager.unlockText, "Lance Grenade débloqué, clic droit pour l'utiliser");
                break;
            case 2:
                _weaponPompe.surchauffeAddLanceGrenade = _shotGunManager.surchauffeLanceGreStats[1];
                break;
            case 3:
                _weaponPompe.grenade.GetComponent<Grenade_Explosion>().force = _shotGunManager.explosionForceLanceGreStats[1];
                _weaponPompe.grenade.GetComponent<Grenade_Explosion>().radius += _shotGunManager.radiusExplosionGre[1];
                _weaponPompe.grenade.GetComponent<Grenade_Explosion>().exlosionRadius += _shotGunManager.radiusExplosionGre[1];
                //augmenter la taille de l'explosion visuel
            break;
        }
        lanceGre.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;
    }
    
    //Glock
    /*public void GlockUpgrade(int UpgradeLevel)
    {
        switch (UpgradeLevel)
        {
            case 1: 
                _weaponGlock.penetrationNumber = _glockManager.penetrationStats[1];
                break;
            case 2:
                _weaponGlock.Degats = _glockManager.damageStats[1];
                break;
            case 3:
                _weaponGlock.fireRate = _glockManager.fireRateStats[1];
                _glockManager.canRafal = true;
                _glockManager.UnlockCompétenceTxt(_glockManager.unlockText, "Tir en rafale débloqué");
                break;
        }
        glock.GetComponent<CheckUpgrade>().upgradeAlreadyHave = UpgradeLevel;
    }*/
    
    
    
    


    private void OnDisable()
    {
        PauseMenu.cursorLock = true;
    }
}
