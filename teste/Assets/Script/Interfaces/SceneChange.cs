using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nameSceneToLoad; //lui donner le nom de la scène à charger

  
    // called via button
    public void changeScene()
    {
        SceneManager.LoadScene(nameSceneToLoad);
    }

    public void LoadLevel(string levelName)
    {
        nameSceneToLoad = levelName;
    }
}
