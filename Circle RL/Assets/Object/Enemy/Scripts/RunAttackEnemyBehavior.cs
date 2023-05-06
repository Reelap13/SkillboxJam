using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAttackEnemyBehavior : EnemyBehavior
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float timeBetweenPerformingAbility;
    [SerializeField] private float distanceForPerformingAbility;
    private AIEnemyMovement movement;
    private Rigidbody2D rb;
    private const int ENEMY_LAYER = ~(5 << 9);//ѕобитова€ маска 9 и 11 слоев(5 - 101) двигаем на 9 влево
    private float timeAfterPerformingAbility;
    RaycastHit2D hit;
    public float Damage => enemy.EnemyPreset.Damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = enemy.AIEnemyMovement;
        timeAfterPerformingAbility = 0;
    }

    private void FixedUpdate()
    {
        timeAfterPerformingAbility += Time.fixedDeltaTime;
        CheckRaycastHit();
    }

    private void CheckRaycastHit()
    {
        if (hit = Physics2D.Raycast(rb.position, ((Vector2)movement.Target.position - rb.position).normalized, distanceForPerformingAbility, ENEMY_LAYER))
        {
            if (hit.collider.tag == Tag.getTag(TagEnum.PLAYER))
            {
                if (timeAfterPerformingAbility >= timeBetweenPerformingAbility)
                {
                    movement.BlockMovement();
                    timeAfterPerformingAbility = 0;
                    enemy.EnemyAbility.PerformAbility(((Vector2)hit.transform.position - rb.position).normalized);
                }
            }
            else
                movement.UnblockMovement();
        }
        else
            movement.UnblockMovement();
    }
}

