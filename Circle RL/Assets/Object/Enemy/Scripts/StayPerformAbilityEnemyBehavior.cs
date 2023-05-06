using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayPerformAbilityEnemyBehavior : EnemyBehavior
{
    [SerializeField] private float timeBetweenSpawning;

    private float timeAfterSpawning;

    private void Awake()
    {
        timeAfterSpawning = 0;
        enemy.AIEnemyMovement?.BlockMovement();
    }

    private void FixedUpdate()
    {
        timeAfterSpawning += Time.deltaTime;
        if (timeAfterSpawning >= timeBetweenSpawning)
        {
            timeAfterSpawning = 0;
            enemy.EnemyAbility.PerformAbility(new Vector2());
        }
    }
}
