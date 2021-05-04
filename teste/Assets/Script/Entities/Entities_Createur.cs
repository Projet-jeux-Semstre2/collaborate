using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entities_Createur : MonoBehaviour
{
    public float timeBeforeCreate, timeMin, timeMax, t;
    private Entities_Manager _entitiesManager;
    public float spawnRadius= 5;

    private void Start()
    {
        _entitiesManager = GetComponent<Entities_Manager>();
        RestartRandomise();
    }


    private void Update()
    {
        t += Time.deltaTime;

        if (t >= timeBeforeCreate)
        {
            SpawnEntities(spawnRadius);
        }
    }

    void RestartRandomise()
    {
        t = 0;
        timeBeforeCreate = Random.Range(timeMin, timeMax); 
    }

    void SpawnEntities(float spawnRadius)
    {
        bool hasCreate = false;
        if (!hasCreate)
        {
            Vector3 rdPosition = Random.insideUnitSphere * spawnRadius + transform.position;
            rdPosition.y = transform.position.y;
            int rdEntities = Random.Range(0, _entitiesManager.EntitiesType.Length);
            GameObject instantiate = Instantiate(_entitiesManager.EntitiesType[rdEntities], rdPosition, Quaternion.identity);
            hasCreate = true;
        }
        RestartRandomise();
    }
}
