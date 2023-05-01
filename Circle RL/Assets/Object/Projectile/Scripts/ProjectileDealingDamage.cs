using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileDealingDamage : MonoBehaviour
{
    [field: SerializeField]
    public Projectile Projectile { get; private set; }
    public TagEnum Team => Projectile.ProjectileParameters.Team;
    public float Damage => Projectile.ProjectileParameters.Damage;
}
