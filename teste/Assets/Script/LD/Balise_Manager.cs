using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balise_Manager : MonoBehaviour
{

    public GameObject[] balises;

    public float nbBaliseFinish;

    public bool allBaliseFinish;
   
    void Start()
    {
        balises = GameObject.FindGameObjectsWithTag("Balise");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var baliseEnd in balises)
        {
            if (baliseEnd.GetComponent<Balise_Fonctionnement>().isCapture && !baliseEnd.GetComponent<Balise_Fonctionnement>().isCount)
            {
                baliseEnd.GetComponent<Balise_Fonctionnement>().isCount = true;
                nbBaliseFinish += 1;
            }
        }

        if (nbBaliseFinish == balises.Length)
        {
            allBaliseFinish = true;
        }

        if (allBaliseFinish)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
