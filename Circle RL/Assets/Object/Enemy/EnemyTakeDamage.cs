using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour, IWeaponVisitor
{
    private EnemyBehavior enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBehavior>();
    }

    public void Visit(ProjectileDealingDamageByTouch deal)
    {
        enemy.TakeDamage(deal.Damage);
    }
}
