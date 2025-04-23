using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    [SerializeField] protected AIEnemy enemy;

    protected float Damage => enemy.EnemyPreset.Damage;

    public abstract void PerformAbility(Vector2 direction);
}
