using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour, IWeaponVisitor
{
    private Enemy enemy;
    private Rigidbody2D rb;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Visit(ProjectileDealingDamageByTouch deal, Vector2 direction)
    {
        rb?.AddForce(direction * deal.PushForce);
        enemy.EnemyParameters.TakeDamage(deal.Damage);
    }

    public void Visit(MeleeAttackGiveDamage deal)
    {
        //throw new System.NotImplementedException();
    }
}
