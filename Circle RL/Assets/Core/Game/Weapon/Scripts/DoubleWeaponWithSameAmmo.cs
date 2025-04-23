using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleWeaponWithSameAmmo : DoubleWeapon
{
    [field: SerializeField]
    public float ReloadTime { get; private set; }
    [field: SerializeField]
    public int AmmoInClip { get; private set; }


    protected override void PreSet()
    {
        base.PreSet();

        WeaponAmmunition weaponAmmunition = new WeaponAmmunition();
        FirstWeapon.SetWeaponAmmunition(weaponAmmunition);
        SecondWeapon.WeaponAttack.OnMakeAttack.AddListener(FirstWeapon.WeaponReload.AbortReload);
        SecondWeapon.WeaponAttack.OnMakeAttack.AddListener(FirstWeapon.WeaponReload.StartReload);
        SecondWeapon.SetWeaponAmmunition(weaponAmmunition);

        FirstWeapon.WeaponReload.ChangereloadTime(ReloadTime);
        SecondWeapon.WeaponReload.ChangereloadTime(float.MaxValue);

        FirstWeapon.SetAmmoInClip(AmmoInClip);
    }
}