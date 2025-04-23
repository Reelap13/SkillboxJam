using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : ProjectileMovement
{
    [SerializeField] private float speed;
    [SerializeField] Projectile projectile;

    private Rigidbody2D _rb;
    private bool isMoving = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            Move();
    }

    private void Move()
    {
        _rb.linearVelocity = Direction.normalized * speed;
    }

    public override void StartMovement()
    {
        isMoving = true;
    }
}
