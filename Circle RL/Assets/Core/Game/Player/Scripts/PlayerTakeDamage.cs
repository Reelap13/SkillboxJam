using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerHealth))]
public class PlayerTakeDamage : MonoBehaviour, IWeaponVisitor
{
    public void Visit(ProjectileDealingDamageByTouch deal, Vector2 direction)
    {
        GetComponent<PlayerHealth>().HitPoint -= deal.Damage;
    }

    public void Visit(MeleeAttackGiveDamage deal)
    {
        GetComponent<PlayerHealth>().HitPoint -= deal.Damage;
    }
}
