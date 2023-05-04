using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabShooting : WeaponAttack
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private int countOfShooting = 1;
    [SerializeField, Range(0, 0.75f)] private float bulletAcceleration = 0;
    [SerializeField] private Transform shootingPoint;
    [SerializeField, Range(0, 90f)] private float scatter = 0;
    private GameObject projectilePref;
    private TemporaryProjectiles temporaryProjectiles;

    private void Awake()
    {
        projectilePref = projectile.gameObject;
    }

    protected override void PerformAttack()
    {
        for (int _ = 0; _ < ShotCost * countOfShooting; ++_)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootingPoint.position).normalized;

            float scatterAngle = Random.Range(-scatter, scatter) * Mathf.PI / 180;
            direction = (new Vector3(direction.x * Mathf.Cos(scatterAngle) - direction.y * Mathf.Sin(scatterAngle), 
                direction.x * Mathf.Sin(scatterAngle) + direction.y * Mathf.Cos(scatterAngle), 
                0)).normalized;

            GameObject pref = projectilePref;
            if (temporaryProjectiles?.ProjectilePref != null)
                pref = temporaryProjectiles.ProjectilePref;

            GameObject newProjectile = Instantiate(pref) as GameObject;
            newProjectile.GetComponent<Transform>().position = shootingPoint.position;

            float angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newProjectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);

            newProjectile.GetComponent<Projectile>().Shoot(new ProjectileParameters(shootingPoint.position, direction, Damage, Team, bulletAcceleration));
        }
    }

    protected override void DisposeAttack()
    {
        //throw new System.NotImplementedException();
    }

    public void AddTemporaryProjectiles(TemporaryProjectiles temporaryProjectiles)
    {
        this.temporaryProjectiles = temporaryProjectiles;
    }
}
