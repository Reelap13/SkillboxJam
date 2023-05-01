using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    [field: SerializeField]
    public Projectile Projectile { get; private set; }

    protected Vector3 StartingPoint => Projectile.ProjectileParameters.StartingPoint;
    protected Vector3 Direction => Projectile.ProjectileParameters.Direction;
    public abstract void StartMovement();
}
