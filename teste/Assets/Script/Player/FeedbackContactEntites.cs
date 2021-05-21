using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FeedbackContactEntites : MonoBehaviour
{
    public PostProcessVolume volume;
    private AutoExposure _autoExposure;
    private Vignette _vignette;
    private ChromaticAberration _chromatic;
    public float autoExposure, vignette, focusDistance, chromatic;
    public float autoExposureMultiply, vignetteMultply, focusMultiply, chromaMultiply;
    public float distance;

    public float maxautoExposure, maxVignette, maxChroma, maxFocus;

    [Header("InitialValue")] 
    public float InitAutoExposure;
    public float InitVignette;
    public float InitChroma;
    public float InitFocus;
    private void Start()
    {
        volume.profile.TryGetSettings(out _autoExposure);
        volume.profile.TryGetSettings(out _vignette);
        volume.profile.TryGetSettings(out _chromatic);
    }

    private void Update()
    {
        

        if (distance <= 10)
        {
            vignette = Mathf.Clamp(vignette, InitVignette, maxVignette * 2);
        }
        else
        {
            autoExposure = Mathf.Clamp(autoExposure, InitAutoExposure, maxautoExposure);
            vignette = Mathf.Clamp(vignette, InitVignette, maxVignette);
            chromatic = Mathf.Clamp(chromatic, InitChroma, maxChroma);
            focusDistance = Mathf.Clamp(focusDistance, maxFocus, InitFocus);
            distance = Mathf.Clamp(distance, 1, Mathf.Infinity);
        }
        
        _autoExposure.minLuminance.value = autoExposure;
        _vignette.intensity.value = vignette;
        _chromatic.intensity.value = chromatic;

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("feedbackZone"))
        {
            
            distance = Vector3.Distance(transform.position, other.transform.position);
            
            autoExposure = autoExposureMultiply/ distance;
            vignette = vignetteMultply/distance;
            chromatic = chromaMultiply / distance;
            if (distance <= 10)
            {
                focusDistance= focusMultiply/ distance;
            }
            else
            {
                focusDistance = Mathf.Lerp(focusDistance, InitFocus, Time.deltaTime);
            }
            
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("feedbackZone"))
        {
            autoExposure = Mathf.Lerp(autoExposure, InitAutoExposure, Time.deltaTime);
            vignette = Mathf.Lerp(vignette, InitVignette, Time.deltaTime);
            chromatic = Mathf.Lerp(chromatic, InitChroma, Time.deltaTime);
            focusDistance= Mathf.Lerp(focusDistance, InitFocus, Time.deltaTime);

        }
    }
}
