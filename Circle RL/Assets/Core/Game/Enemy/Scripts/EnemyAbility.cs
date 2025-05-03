using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAbility : MonoBehaviour
{
    [NonSerialized] public UnityEvent OnKillingPlayer = new();
    [NonSerialized] public UnityEvent<float> OnMakingDamage = new();

    [SerializeField] protected AIEnemy enemy;

    protected float Damage => enemy.EnemyPreset.Damage;

    public abstract void PerformAbility(Vector2 direction);
}
