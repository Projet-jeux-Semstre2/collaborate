using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    public float rangeSpawn = 50;
    public GameObject[] allEntitites;
    public SetUPScene _setUpScene;
    
    private float intSPawn;
    private Vector3 rd;
    private GameObject[] spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {

        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        
        _setUpScene = GameObject.FindWithTag("ManagerScene").GetComponent<SetUPScene>();
        intSPawn = _setUpScene.GetComponent<SetUPScene>().nrb / spawnPoint.Length;
       for (int i = 0; i < intSPawn; i++)
       {
           rd = Random.insideUnitSphere * rangeSpawn + transform.position;
           rd.y = transform.position.y;
          RandomCreate(rd);
       }
    }

    private void RandomCreate(Vector3 var)
    {
        int random = Random.Range(0, allEntitites.Length);
        Instantiate(allEntitites[random],var,Quaternion.identity);
    }
}
