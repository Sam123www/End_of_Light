using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool playerCheck;
    public float radius;
    public LayerMask playerMask;
    public Transform playerTransform;
    protected Rigidbody2D rb;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        PhysicsCheck();
    }
    protected void PhysicsCheck()
    {   
        playerCheck = Physics2D.OverlapCircle(transform.position, radius, playerMask);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
