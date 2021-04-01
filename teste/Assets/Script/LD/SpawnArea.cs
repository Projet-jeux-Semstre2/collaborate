using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    public float rangeSpawn = 50;
    [Tooltip("Permet de choisir aléatoirement une forme d'entités")]
    public GameObject[] allEntitites;
    public SetUPScene _setUpScene;
    
    private float _instSpawn;
    private Vector3 _rd;
    private GameObject[] _spawnPoint;
    private float _nbAtStart;

    public float t_beforeRespawn = 15f;
    private float _t;
    

    // Start is called before the first frame update
    void Start()
    {

        _spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        
        _setUpScene = GameObject.FindWithTag("ManagerScene").GetComponent<SetUPScene>();
        _nbAtStart = _setUpScene.GetComponent<SetUPScene>().numberEntitiesAtStart;
        _instSpawn =  _nbAtStart / _spawnPoint.Length;
        Spawn(_instSpawn);
    }

    private void RandomCreate(Vector3 var)
    {
        int random = Random.Range(0, allEntitites.Length);
        Instantiate(allEntitites[random],var,Quaternion.identity);
    }

    private void FixedUpdate()
    {
        _t += Time.deltaTime;
        if (_t >= t_beforeRespawn)
        {
            Respawn();
            _t = 0;
        }
        
    }

    void Spawn(float nbToSpawn)
    {
        for (int i = 0; i < nbToSpawn; i++)
        {
            _rd = Random.insideUnitSphere * rangeSpawn + transform.position;
            _rd.y = transform.position.y;
            RandomCreate(_rd);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeSpawn);
    }



    void Respawn()
    {
        if (ObjectifExtermination.nrbEntités.Count < _nbAtStart)
        {
            float nb = (_nbAtStart - ObjectifExtermination.nrbEntités.Count) / _spawnPoint.Length;
            Spawn(nb);
        }
    }
}
