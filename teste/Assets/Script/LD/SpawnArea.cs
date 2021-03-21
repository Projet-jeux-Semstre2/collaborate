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
    
    private float _instSpawn;
    private Vector3 _rd;
    private GameObject[] _spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {

        _spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        
        _setUpScene = GameObject.FindWithTag("ManagerScene").GetComponent<SetUPScene>();
        _instSpawn = _setUpScene.GetComponent<SetUPScene>().nrb / _spawnPoint.Length;
       for (int i = 0; i < _instSpawn; i++)
       {
           _rd = Random.insideUnitSphere * rangeSpawn + transform.position;
           _rd.y = transform.position.y;
           RandomCreate(_rd);
       }
    }

    private void RandomCreate(Vector3 var)
    {
        int random = Random.Range(0, allEntitites.Length);
        Instantiate(allEntitites[random],var,Quaternion.identity);
    }
}
