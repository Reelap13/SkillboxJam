using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileDealingDamage : MonoBehaviour
{
    [field: SerializeField]
    protected Projectile Projectile { get; private set; }
    public TagEnum Team => Projectile.ProjectileParameters.Team;
    public float Damage => Projectile.ProjectileParameters.Damage;
    public float PushForce => Projectile.ProjectileParameters.PushForce;
}
