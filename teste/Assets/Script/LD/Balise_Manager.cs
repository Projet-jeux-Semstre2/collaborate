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
            Balise_Fonctionnement baliseFonctionnement = baliseEnd.GetComponent<Balise_Fonctionnement>();
            if (baliseFonctionnement.isCapture && !baliseFonctionnement.isCount)
            {
                baliseFonctionnement.isCount = true;
                nbBaliseFinish++;
            }
        }

        if (nbBaliseFinish == balises.Length)
        {
            allBaliseFinish = true;
        }

        if (allBaliseFinish)
        {
            SceneManager.LoadScene("Win");
        }

        
        
        
    }
    
    
    


    
    
    
    
}
