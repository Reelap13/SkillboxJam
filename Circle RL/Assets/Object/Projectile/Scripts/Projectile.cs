using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{

    [field: SerializeField]
    public ProjectileMovement ProjectileMovement;
    [field: SerializeField]
    public  ProjectileDealingDamage projectileDealingDamage;

    public ProjectileParameters ProjectileParameters { get; private set; }
    public void Shoot(ProjectileParameters parameters)
    {
        ProjectileParameters = parameters;
        ProjectileMovement.StartMovement();
    }
}

[Serializable]
public struct ProjectilePreset
{
    public float speed;
}

[Serializable]
public struct ProjectileParameters
{
    public Vector2 StartingPoint;
    public Vector2 Direction;
    public float Damage;
    public TagEnum Team;
    public float Acceleration;

    public ProjectileParameters(Vector2 startingPoint, Vector2 direction, float damage, TagEnum team, float acceleration)
    {
        StartingPoint = startingPoint;
        Direction = direction;
        Damage = damage;
        Team = team;
        Acceleration = acceleration;
    }
}