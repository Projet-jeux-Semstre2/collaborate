using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUpgrade : MonoBehaviour
{
    public int upgradeAlreadyHave;
    public GameObject[] buttonChildren;

    private void OnEnable()
    {
        switch (upgradeAlreadyHave)
        {
            case 0:
                buttonChildren[0].SetActive(true);
                break;
            case 1:
                buttonChildren[1].SetActive(true);
                break;
            case 2:
                buttonChildren[2].SetActive(true);
                break;
        }
    }

    private void OnDisable()
    {
        foreach (var button in buttonChildren)
        {
            button.SetActive(false);
        }
    }
}
