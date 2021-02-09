using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDetection : MonoBehaviour
{
    public GameObject _myParent;

    public float radiusDivide;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coredetection") && _myParent.GetComponent<Core_Manager>().myEntities.Count < other.transform.parent.GetComponent<Core_Manager>().myEntities.Count)
        {
            foreach (var test in _myParent.GetComponent<Core_Manager>().myEntities)
            {
                test.GetComponent<Entities_Manager>().hasCore = false;
            }
            Destroy(_myParent);
        }
    }

    private void Update()
    {
        //permet d'avoir la zone de détéction avec une taille en rapport aux nombre d'entités que comporte le corps. Le radius ne peut pas etre inferieur à 6.5f;
        if (_myParent.GetComponent<Core_Manager>().myEntities.Count / radiusDivide >= 6.5f)
        {
            GetComponent<SphereCollider>().radius = _myParent.GetComponent<Core_Manager>().myEntities.Count / radiusDivide;
        }
        else
        {
            GetComponent<SphereCollider>().radius = 6.5f;
        }
        
        //Faire des paliers pour réduire le radius qui augmente de facon exponentielle avce le nb d'entité
    }
}
