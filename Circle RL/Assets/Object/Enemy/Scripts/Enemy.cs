using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour
{

    static public UnityEvent summonEnemy = new UnityEvent();
    static public UnityEvent destroyEnemy = new UnityEvent();

    [field: SerializeField]
    public AIEnemyMovement AIEnemyMovement { get; private set; }
    [field: SerializeField]
    public EnemyBehavior EnemyBehavior { get; private set; }
    [field: SerializeField]
    public EnemyAbility EnemyAbility { get; private set; }

    [field: SerializeField] 
    public EnemyPreset EnemyPreset { get; private set; }

    public EnemyParameters EnemyParameters { get; private set; }
    void Start()
    {
        EnemyParameters = new EnemyParameters(EnemyPreset);
        EnemyParameters.OnDieing.AddListener(Die);
        EnemyPreset.ChangeTeam(TagEnum.ENEMY);
        summonEnemy.Invoke();
    }

    private void Die()
    {
        destroyEnemy.Invoke();
        Destroy(gameObject);
    }

    public bool IsAlive
    {
        get
        {
            return EnemyParameters.IsAlive();
        }
    }
}

[Serializable]
public struct EnemyPreset
{
    public float StartHealth;
    public float StartShield;
    public float Damage;
    public TagEnum Team;

    public void ChangeTeam(TagEnum team)
    {
        Team = team;
    }
}


public class EnemyParameters
{
    public UnityEvent OnDieing = new UnityEvent();
    public UnityEvent OnBrokingShield = new UnityEvent();

    public float Health;
    public float Shield;

    public EnemyParameters(EnemyPreset preset)
    {
        Health = preset.StartHealth;
        Shield = preset.StartShield;
    }

    public void TakeDamage(float damage)
    {
        if (damage >= Shield)
        {
            damage -= Shield;
            Shield = 0;
        }
        else
        {
            Shield -= damage;
            return;
        }

        if (damage >= Health)
        {
            Health = 0;
            OnDieing.Invoke();
        }
        else
            Health -= damage;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}