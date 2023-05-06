using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemyAbility : EnemyAbility
{
    [SerializeField] private GameObject enemyPref;
    private Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public override void PerformAbility(Vector2 direction)
    {
        GameObject newEnemy = Instantiate(enemyPref) as GameObject;
        newEnemy.transform.position = tr.position;
    }
}
