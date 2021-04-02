using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public string nameSceneToLoad; //lui donner le nom de la scène à charger
   public void GameOver()
   {
      SceneManager.LoadScene(nameSceneToLoad);
   }

   public void GameWin()
   {
      SceneManager.LoadScene(nameSceneToLoad);
   }
}
