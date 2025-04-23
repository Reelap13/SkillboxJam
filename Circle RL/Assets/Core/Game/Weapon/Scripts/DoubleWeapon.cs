using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleWeapon : MonoBehaviour
{
    [field: SerializeField]
    public Weapon FirstWeapon { get; private set; }
    [field: SerializeField]
    public Weapon SecondWeapon { get; private set; }

    private void Awake()
    {
        PreSet();
    }

    protected virtual void PreSet()
    {
        FirstWeapon.WeaponAttack.SetMouseButton(MouseButton.LEFT);
        SecondWeapon.WeaponAttack.SetMouseButton(MouseButton.RIGHT);

        FirstWeapon.WeaponAttack.OnAttackStarted.AddListener(BlockSecondWeapon);
        FirstWeapon.WeaponAttack.OnAttackEnded.AddListener(UnblockSecondWeapon);

        SecondWeapon.WeaponAttack.OnAttackStarted.AddListener(BlockFirstWeapon);
        SecondWeapon.WeaponAttack.OnAttackEnded.AddListener(UnblockFirstWeapon);
    }

    private void BlockFirstWeapon() => FirstWeapon.BlockAttack();
    private void UnblockFirstWeapon() => FirstWeapon.UnblockAttack();
    private void BlockSecondWeapon() => SecondWeapon.BlockAttack();
    private void UnblockSecondWeapon() => SecondWeapon.UnblockAttack();

}
