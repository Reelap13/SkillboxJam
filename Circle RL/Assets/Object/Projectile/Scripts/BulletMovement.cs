using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : ProjectileMovement
{
    [SerializeField] private float speed;
    [SerializeField] Projectile projectile;

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
    }

    private void Move()
    {
        _rb.velocity = Direction.normalized * speed;
    }

    public override void StartMovement()
    {
        isMoving = true;
        speed *= Random.Range(1 - Acceleration, 1 + Acceleration);
    }
}
