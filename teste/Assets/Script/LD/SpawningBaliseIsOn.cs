using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
public class SpawningBaliseIsOn : MonoBehaviour
{
    public GameObject Entities;
    public List<GameObject> spawnerBalise;
    private Balise_Fonctionnement _baliseFonctionnement;
    private Entities_Manager _entitiesManager;
    public bool baliseOn;
    public bool isCpatureOn;
    public bool canSpawnRate;
    public float waitTime = 0.4f;
    public bool stopIspawn;
    private int amount;

    public float nrbDifficulté = 40.0f;
   
    private int Difficulté; // la valeur de difficultés chosie au menu.
    void Start()
    {
        stopIspawn = true;
        _entitiesManager = Entities.GetComponent<Entities_Manager>();
        canSpawnRate = true;
        // les spawner sont ne font pas spaawn des montres

    }

    // Update is called once per frame
    void Update()
    {
        if (baliseOn && !isCpatureOn && canSpawnRate)
        {
            if (stopIspawn)
            {
                if (amount <= nrbDifficulté)
                {
                    for (int i = 0; i < nrbDifficulté; i++)
                    {
                        SpawningRépartition();
                    }
                }
            }
        }

        if (!baliseOn)
        {
            amount = 0;
        }
        
    }

    IEnumerator Timer()
    {
        canSpawnRate = false;
        yield return new WaitForSeconds(waitTime);
        canSpawnRate = true;
    }

    void SpawningRépartition()
    {
        if (canSpawnRate)
        {
            // on récupère les spawneur associées a la balise, on récupe leur transform 
            int spawnPos = Random.Range(0,spawnerBalise.Count);
        
            int rdEntities = Random.Range(0, _entitiesManager.EntitiesType.Length);// copie du tableua des entités
            Transform spawnPoint = spawnerBalise[spawnPos].transform;
            GameObject instantiate = Instantiate(_entitiesManager.EntitiesType[rdEntities], spawnPoint.position, Quaternion.identity); // fait spawn une entités sur un des spawneur.*
            StartCoroutine(Timer());
            amount++;
        }
       
    }
}

