using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttackAnim : MonoBehaviour
{
    [field: SerializeField] 
    public MeleeAttack MeleeAttack { get; private set; }

    protected float AnimTime => MeleeAttack.MeleeAttackPreset.TimeBeforeAttack; 

    public abstract void StartAnim();
}
