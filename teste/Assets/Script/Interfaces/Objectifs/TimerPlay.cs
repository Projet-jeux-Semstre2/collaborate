using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerPlay : MonoBehaviour
{

    public Text timerText;
    private float timer;

    public float timeTotal;
    // Start is called before the first frame update
    void Start()
    {
        timeTotal = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.0f)
        {
            timeTotal++;
            timer = timer - 1.0f;
        }
        timerText.text = "temps =  " + timeTotal;
    }
}
