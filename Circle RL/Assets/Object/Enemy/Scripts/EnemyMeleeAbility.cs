using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAbility : EnemyAbility
{
    [SerializeField] private GameObject meleeAttackPref;
    [SerializeField] private float radius;
    [SerializeField] private float pushForce;

    private Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public override void PerformAbility(Vector2 direction)
    {
        GameObject newMeleeAttack = Instantiate(meleeAttackPref) as GameObject;
        newMeleeAttack.transform.parent = tr;

        MeleeAttack meleeAttack = newMeleeAttack.GetComponent<MeleeAttack>();
        meleeAttack.SetPush(new Push2D(tr, pushForce));
        meleeAttack.SetParameters(new MeleeAttackParameters(Damage, tr, enemy.AIEnemyMovement.Target, radius));
    }
}
