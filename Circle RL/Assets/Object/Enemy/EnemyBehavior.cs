using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyBehavior : MonoBehaviour
{

    static public UnityEvent summonEnemy = new UnityEvent();
    static public UnityEvent destroyEnemy = new UnityEvent();

    [field: SerializeField] 
    public EnemyPreset EnemyPreset { get; private set; }

    private float health;
    void Start()
    {
        health = EnemyPreset.StartHealth;
        summonEnemy.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        if (health <= 0)
        {
            destroyEnemy.Invoke();
            Destroy(gameObject);
        }
    }

    public bool IsAlive
    {
        get
        {
            return health > 0;
        }
    }
}

[Serializable]
public struct EnemyPreset
{
    public float StartHealth;
    public float Damage;
}