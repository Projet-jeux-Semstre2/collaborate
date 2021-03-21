using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectifExtermination : MonoBehaviour
{
    public Text textExtermination;
    private List<GameObject> nrbEntités;
    public static float nrbTotal;

    public GameManager _gameManager;

   

    // Update is called once per frame
    void Update()
    {
        textExtermination.text = "Nombre Entités = " + nrbTotal;
        if (nrbTotal <= 0)
        {
            _gameManager.GetComponent<GameManager>().GameWin();
        }
    }
}
