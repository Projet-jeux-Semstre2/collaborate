using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodSonsDeOrely : MonoBehaviour
{
    public string on;
    public string off;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.PlayOneShot(on);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            FMODUnity.RuntimeManager.PlayOneShot(off);
        }
    }
}
