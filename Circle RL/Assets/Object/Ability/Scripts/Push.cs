using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : Ability
{
    [SerializeField] private Rigidbody2D entity;
    [SerializeField, Space] private float force;

    public override void use()
    {
        Vector2 direction = entity.velocity;
        if (direction.magnitude == 0)
            direction = new Vector2(1, 0);
        direction = direction.normalized;

        entity.AddForce(direction * force);
    }
}
