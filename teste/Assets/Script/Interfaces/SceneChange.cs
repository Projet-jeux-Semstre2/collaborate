using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string nameSceneToLoad; //lui donner le nom de la scène à charger

    public void playMusic(string zik)
    {
        FMODUnity.RuntimeManager.PlayOneShot(zik);
    }
    // called via button
    public void changeScene(float waitTime)
    {
        StartCoroutine(goToScene(waitTime));
    }

    public void LoadLevel(string levelName)
    {
        nameSceneToLoad = levelName;
    }

    IEnumerator goToScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nameSceneToLoad);
    }
}
