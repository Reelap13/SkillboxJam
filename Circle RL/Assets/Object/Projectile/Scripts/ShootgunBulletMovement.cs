using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunBulletMovement : ProjectileMovement
{
    [SerializeField] private float speed;
    [SerializeField] Projectile projectile;
    [SerializeField] float slowDown;

    private float Acceleration => projectile.ProjectileParameters.Acceleration;
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
        
        speed = Math.Max(speed - slowDown * Time.fixedDeltaTime, 0);
    }

    private void Move()
    {
        _rb.velocity = Direction.normalized * speed;
    }

    public override void StartMovement()
    {
        isMoving = true;
        speed *= UnityEngine.Random.Range(1 - Acceleration, 1 + Acceleration);
    }
}
