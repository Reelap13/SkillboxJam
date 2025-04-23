using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityBomber : EnemyAbility
{
    [SerializeField] private GameObject meleeAttackPref;
    [SerializeField] private float slowdownCoefficient = 1;
    [SerializeField] private float slowdownTime = 0;
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
        meleeAttack.SetParameters(new MeleeAttackParameters(Damage, tr, enemy.AIEnemyMovement.Target, 0));
        meleeAttack.MeleeAttackGiveDamage.OnGiveDamage.AddListener(KillEnemy);
        new EnemySlow(slowdownCoefficient, slowdownTime, enemy.AIEnemyMovement);
        void KillEnemy()
        {
            enemy.EnemyParameters.KillEnemy();
        }
    }
}
