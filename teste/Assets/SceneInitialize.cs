using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour
{
   public GameObject SetUp;


   private void OnEnable()
   {
      Instantiate(SetUp, transform.transform.position, Quaternion.identity);
   }
}
