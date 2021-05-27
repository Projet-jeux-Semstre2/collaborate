using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDistanceFeedback : MonoBehaviour
{
    public Core_Attack _coreAttack;
    public float coefProportionnel;
    public float scaleInit;
    private float distAttackInit;
    private RaycastHit hit;
    public LayerMask normalCheck;
    private GameObject normal;


    void Start()
    {
        scaleInit = transform.localScale.x;
        distAttackInit =_coreAttack.distanceAttack ;
        coefProportionnel = scaleInit / distAttackInit;
    }


    void Update()
    {
        float finalscale = _coreAttack.temporairBase * coefProportionnel;
        transform.localScale = new Vector3(finalscale, finalscale, finalscale);
    }
}
