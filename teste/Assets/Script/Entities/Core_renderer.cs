using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_renderer : MonoBehaviour
{
    public Transform core;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    public Material[] coreMaterials;
    public Light light;
    public Color[] colors;
    private Core_Manager _coreManager;
    private Material myMaterial;

    public string most;

    private void Start()
    {
        _coreManager = core.GetComponent<Core_Manager>();
        myMaterial = GetComponent<MeshRenderer>().material;
    }


    private void Update()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(core.position.x, core.position.y + 0.5f,core.position.z), ref velocity, smoothTime);

        ChangeColor();
    }
    
    public void ChangeColor()
    {
        
        if(_coreManager.tankNb > _coreManager.agressifNb && _coreManager.tankNb > _coreManager.vifNb && _coreManager.tankNb > _coreManager.createurNb)
        {
            if (myMaterial != coreMaterials[0])
            {
                most = "Tank";
                light.color = colors[0];
                gameObject.GetComponent<MeshRenderer>().material = coreMaterials[0];
                myMaterial = coreMaterials[0];
            }
        }

        if (_coreManager.agressifNb > _coreManager.tankNb && _coreManager.agressifNb > _coreManager.vifNb && _coreManager.agressifNb > _coreManager.createurNb)
        {
            if (myMaterial != coreMaterials[1])
            {
                most = "Agressif";
                light.color = colors[1];
                GetComponent<MeshRenderer>().material = coreMaterials[1];
                myMaterial = coreMaterials[1];
            }
        }
        
        if (_coreManager.vifNb > _coreManager.tankNb && _coreManager.vifNb > _coreManager.agressifNb && _coreManager.vifNb > _coreManager.createurNb)
        {
            if (myMaterial != coreMaterials[2])
            {
                most = "Vif";
                light.color = colors[2];
                GetComponent<MeshRenderer>().material = coreMaterials[2];
                myMaterial = coreMaterials[2];
            }
        }
        
        if (_coreManager.createurNb > _coreManager.tankNb && _coreManager.createurNb > _coreManager.agressifNb && _coreManager.createurNb > _coreManager.vifNb)
        {
            if (myMaterial != coreMaterials[3])
            {
                most = "Crea";
                light.color = colors[3];
                GetComponent<MeshRenderer>().material = coreMaterials[3];
                myMaterial = coreMaterials[3];
            }
        }
        
        if(_coreManager.tankNb == _coreManager.agressifNb && _coreManager.tankNb == _coreManager.createurNb && _coreManager.tankNb  == _coreManager.vifNb)
        {
            if (myMaterial != coreMaterials[4])
            {
                most = "equals";
                light.color = colors[4];
                GetComponent<MeshRenderer>().material = coreMaterials[4];
                myMaterial = coreMaterials[4];
            }
        }
    }
}
