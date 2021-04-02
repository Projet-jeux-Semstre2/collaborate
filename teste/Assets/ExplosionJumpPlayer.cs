using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionJumpPlayer : MonoBehaviour
{
    public float radius, force;
    public LayerMask LayerMask;


    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, LayerMask);
        foreach (Collider collide in colliders)
        {
            if (collide.gameObject.CompareTag("Player") && transform.position.y < collide.transform.position.y)
            {
                collide.GetComponent<Player>().moveDirection.y = force;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
