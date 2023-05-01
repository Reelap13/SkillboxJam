using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnChangeAmmo = new UnityEvent();
    [HideInInspector] public UnityEvent OnReloadAmmo = new UnityEvent();

    [field: SerializeField]
    public WeaponPreset WeaponPreset { get; private set; } = new WeaponPreset();
    public WeaponAmmunition WeaponAmmunition { get; private set; } = new WeaponAmmunition();

    [field: SerializeField]
    public WeaponAttack WeaponAttack { get; private set; }
    [field: SerializeField]
    public WeaponReload WeaponReload{ get; private set; }
    [HideInInspector] public bool IsAccessToAttack { get; private set; } = true;

    private void Awake()
    {
        WeaponAmmunition.Reload(WeaponPreset);
    }

    public void BlockAttack() => IsAccessToAttack = false;
    public void UnblockAttack() => IsAccessToAttack = true;
    public void SetAmmunition(WeaponAmmunition ammunition)
    {
        WeaponAmmunition = ammunition;
        OnChangeAmmo.Invoke();
    }
    public void SpendAmmo(int ammo)
    {
        WeaponAmmunition.SpendAmmo(ammo);
        OnChangeAmmo.Invoke();
    }
    public void ReloadWeapon()
    {
        WeaponAmmunition.Reload(WeaponPreset);
        OnReloadAmmo.Invoke();
    }
}

[Serializable]
public class WeaponPreset
{
    public int ammoInClip;
    public int shotCost;
    public float damage;
    public TagEnum Team;
}


[Serializable]
public class WeaponAmmunition
{
    public int Ammo;

    public void SpendAmmo(int spendedAmmo)
    {
        Ammo -= spendedAmmo;
        if (Ammo < 0)
            Ammo = 0;
    }

    public void Reload(WeaponPreset preset)
    {
        Ammo = preset.ammoInClip;
    }
}