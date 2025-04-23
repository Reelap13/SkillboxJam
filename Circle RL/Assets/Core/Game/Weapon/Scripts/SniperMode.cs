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
        StartCoroutine("TimeSlowdown");

        weapon.AddTemporaryProjectiles(new TemporaryProjectiles(temporaryProjectilesPref, temporaryProjectilesTime));
    }

    protected override void DisposeAttack()
    {
        //throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private IEnumerator TimeSlowdown()
    {
        Time.timeScale = decelerationFactor;
        yield return new WaitForSeconds(timeDilation);
        Time.timeScale = 1;
    }
}