using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFeedback : MonoBehaviour
{
    public Vector2 speedClamp;
    public float force;
    public bool shakeDuringTime;
    public float time;

    bool shake;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;

        if (!shakeDuringTime)
            shake = true;
    }

    private void Update()
    {
        if(shake)
        {
            float speedX = Random.Range(speedClamp.x, speedClamp.y);
            float speedY = Random.Range(speedClamp.x, speedClamp.y);
            transform.localPosition = startPos + new Vector3(Mathf.Sin(Time.time * speedX) * (force * Time.deltaTime), Mathf.Sin(Time.time * speedY) * (force * Time.deltaTime), 0);
        }
    }

    public void PlayShake()
    {
        if (shakeDuringTime)
            StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        shake = true;
        yield return new WaitForSeconds(time);
        shake = false;

        transform.localPosition = startPos;
    }
}
