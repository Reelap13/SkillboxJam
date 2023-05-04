using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMode : WeaponAttack
{
    [SerializeField] private float timeDilation;
    [SerializeField] private float decelerationFactor;

    [SerializeField] private PrefabShooting weapon;
    [SerializeField, Space] private GameObject temporaryProjectilesPref;
    [SerializeField] private float temporaryProjectilesTime;

    protected override void PerformAttack()
    {
        //slow down time by timeDilation with decelerationFactor

        weapon.AddTemporaryProjectiles(new TemporaryProjectiles(temporaryProjectilesPref, temporaryProjectilesTime));
    }

    protected override void DisposeAttack()
    {
        //throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        //comeback normal time
    }
}