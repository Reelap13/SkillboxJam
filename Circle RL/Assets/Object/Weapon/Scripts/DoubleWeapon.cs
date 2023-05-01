using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleWeapon : MonoBehaviour
{
    [fiald: SerializeField]
    public Weapon FirstWeapon;
    [fiald: SerializeField]
    public Weapon SecondWeapon;

    private void Awake()
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
