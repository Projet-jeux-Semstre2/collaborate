using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectifExtermination : MonoBehaviour
{
    public Text textExtermination;
    public static List<GameObject> nrbEntités = new List<GameObject>();

    public GameManager _gameManager;

   

    // Update is called once per frame
    void Update()
    {
        textExtermination.text = "Nombre Entités = " + nrbEntités.Count;
        if (nrbEntités.Count <= 0)
        {
            _gameManager.GameWin();
        }
    }
}
