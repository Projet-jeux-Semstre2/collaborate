using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDestroy : MonoBehaviour
{
    public float radius = 10.0f;
    public float force = 50.0f;
    public float upwardModifier = 2.0f;
    public float time = 1.0f;
    public LayerMask layerMaskExplosion;
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMaskExplosion);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, upwardModifier, ForceMode.Impulse);
            }
        }
        Destroy(gameObject,time);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
