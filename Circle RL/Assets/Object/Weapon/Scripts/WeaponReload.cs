using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{
    [field: SerializeField]
    public Weapon Weapon { get; private set; }
    
    [field: SerializeField]
    public float ReloadTime;

    [SerializeField, Range(0, 1)] private float fastReload;
    [SerializeField, Range(0, 1)] private float penaltyReload;

    private bool reloadInProcess = false;
    private float timeAfterReload;

    private void Awake()
    {
        Weapon.WeaponAttack.OnMakeAttack.AddListener(AbortReload);
        Weapon.WeaponAttack.OnMakeAttack.AddListener(StartReload);
    }

    private void Update()
    {
        if (reloadInProcess)
        {
            timeAfterReload += Time.deltaTime;

            if (timeAfterReload >= ReloadTime)
                EndReload();
        }
    }

    public void StartReload()
    {
        if (reloadInProcess)
            return;

        reloadInProcess = true;
        timeAfterReload = 0;
    }

    private void AbortReload()
    {
        reloadInProcess = false;
    }

    private void EndReload()
    {
        reloadInProcess = false;
        Weapon.ReloadWeapon();
    }
}
