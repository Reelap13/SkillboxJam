using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackMovement : MonoBehaviour
{
    [SerializeField] private MeleeAttack MeleeAttack;

    protected MeleeAttackParameters Parameters => MeleeAttack.MeleeAttackParameters;

    private Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (Parameters.Target == null || Parameters.Attacker == null)
            return;

        Vector3 direction = (Parameters.Target.position - Parameters.Attacker.position).normalized;

        tr.position = Parameters.Attacker.position + direction * Parameters.Radius;

        float angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);

    }
}
