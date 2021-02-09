using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDetection : MonoBehaviour
{
    public GameObject _myParent;

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
}
