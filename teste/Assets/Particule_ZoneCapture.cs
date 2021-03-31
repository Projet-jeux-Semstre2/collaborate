using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Particule_ZoneCapture : MonoBehaviour
{
    
    
    [Header("ParticleSystem")]
    public ParticleSystem circleRuban1, circleRuban1Clone;

    public ParticleSystem circleRuban2, circleRuban2Clone;
    
    public ParticleSystem circleRuban3, circleRuban3Clone;
    
    private float _circleDuration1, _circleDuration2, _circleDuration3;

    [Header("Cercle au sol")]
    public float circleRotationSpeed;
    public GameObject CircleZone;

    private void Start()
    {
        _circleDuration1 = circleRuban1.main.duration;
        _circleDuration2 = circleRuban2.main.duration;
        _circleDuration3 = circleRuban3.main.duration;
        
        circleRuban1.gameObject.SetActive(true);
        circleRuban2.gameObject.SetActive(true);
        circleRuban3.gameObject.SetActive(true);

    }
    

    private void Update()
    {
        CircleRotation();
        
        ParticleRubanRestart(circleRuban1, _circleDuration1, circleRuban1Clone);
        ParticleRubanRestart(circleRuban2, _circleDuration2, circleRuban2Clone);
        ParticleRubanRestart(circleRuban3, _circleDuration3, circleRuban3Clone);
    }


    void CircleRotation()
    {
        CircleZone.transform.Rotate(0,0,circleRotationSpeed * Time.deltaTime, Space.Self);
    }


    void ParticleRubanRestart(ParticleSystem ruban, float duration, ParticleSystem rubanClone)
    {
        
        //Ruban principal
        if (ruban.time >= duration - 5f && ruban.gameObject.activeSelf)
        {
            rubanClone.gameObject.SetActive(true);
        }

        if (ruban.time == duration && ruban.gameObject.activeSelf)
        {
            ruban.gameObject.SetActive(false);
        }

        //Ruban Clone
        if (rubanClone.time >= duration - 5f && rubanClone.gameObject.activeSelf)
        {
            ruban.gameObject.SetActive(true);
        }
        
        if (rubanClone.time == duration && rubanClone.gameObject.activeSelf)
        {
            rubanClone.gameObject.SetActive(false);
        }
        
    }
    
    
}
