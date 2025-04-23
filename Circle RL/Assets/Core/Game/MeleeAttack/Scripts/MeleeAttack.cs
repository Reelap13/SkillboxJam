using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttack : MonoBehaviour
{
    [field: SerializeField]
    public MeleeAttackMovement MeleeAttackMovement;
    [field: SerializeField]
    public MeleeAttackAnim MeleeAttackAnim;
    [field: SerializeField]
    public MeleeAttackGiveDamage MeleeAttackGiveDamage;
    [field: SerializeField]
    public MeleeAttackPreset MeleeAttackPreset;

    public MeleeAttackParameters MeleeAttackParameters = new MeleeAttackParameters();
    public Push2D Push { get; private set; }

    public void SetParameters(MeleeAttackParameters parameters)
    {
        MeleeAttackParameters.ChangeValue(parameters);
        MeleeAttackAnim.StartAnim();
    }

    public void SetPush(Push2D push)
    {
        Push.ChangeValue(push);
    }

}


[Serializable]
public struct MeleeAttackPreset
{
    public float TimeBeforeAttack;
    public TagEnum Team;
}

public struct MeleeAttackParameters
{
    public float Damage;
    public Transform Attacker;
    public Transform Target;
    public float Radius;

    public MeleeAttackParameters(float damage, Transform attacker, Transform target, float radius){
        Damage = damage;
        Attacker = attacker;
        Target = target;
        Radius = radius;
    }

    public void ChangeValue(MeleeAttackParameters parameters)
    {
        Damage = parameters.Damage;
        Attacker = parameters.Attacker;
        Target = parameters.Target;
        Radius = parameters.Radius;
    }
}

public struct Push2D
{
    public Transform StartPoint;
    public float Force;

    public Push2D(Transform startPoint, float force)
    {
        StartPoint = startPoint;
        Force = force;
    }

    public void ChangeValue(Push2D push)
    {
        StartPoint = push.StartPoint;
        Force = push.Force;
    }
}