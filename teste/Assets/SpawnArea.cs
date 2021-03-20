using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public float rangeSpawn = 50;
    public GameObject[] allEntitites;
    public SetUPScene _setUpScene;
    
    private float intSPawn;
    private Vector3 rd;

    
    // Start is called before the first frame update
    void Start()
    {
        intSPawn = _setUpScene.GetComponent<SetUPScene>().NombreEntités;
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
