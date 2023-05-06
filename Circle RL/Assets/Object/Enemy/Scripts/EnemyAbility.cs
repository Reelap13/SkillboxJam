using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    [SerializeField] protected Enemy enemy;

    protected float Damage => enemy.EnemyPreset.Damage;

    public abstract void PerformAbility(Vector2 direction);
}
