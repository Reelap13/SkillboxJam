using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : ProjectileMovement
{
    [SerializeField] private float speed;

    private Transform _tr;
    private bool isMoving = false;
    private void Awake()
    {
        _tr = GetComponent<Transform>();
    }

    private void Update()
    {
        if (isMoving)
            Move();
    }

    private void Move()
    {
        _tr.position = Vector3.MoveTowards(_tr.position, _tr.position + Direction * speed, speed * Time.deltaTime);
    }

    public override void StartMovement()
    {
        isMoving = true;
    }
}
