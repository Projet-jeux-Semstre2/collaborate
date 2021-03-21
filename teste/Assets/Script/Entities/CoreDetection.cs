using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDetection : MonoBehaviour
{
    public GameObject _myParent;
    private SphereCollider _sphereCollider;
    private Core_Manager _coreManager;

    public float radiusDivide;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _coreManager = _myParent.GetComponent<Core_Manager>();
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
        if (_coreManager.myEntities.Count / radiusDivide >= 6.5f)
        {
            _sphereCollider.radius = _coreManager.myEntities.Count / radiusDivide;
        }
        else
        {
            _sphereCollider.radius = 6.5f;
        }
        
        //Faire des paliers pour réduire le radius qui augmente de facon exponentielle avce le nb d'entité
    }
}
