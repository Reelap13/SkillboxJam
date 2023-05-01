using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabShooting : WeaponAttack
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform shootingPoint;
    private GameObject projectilePref;

    private void Awake()
    {
        projectilePref = projectile.gameObject;
    }

    protected override void PerformAttack()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootingPoint.position).normalized;

        GameObject newProjectile = Instantiate(projectilePref) as GameObject;
        newProjectile.GetComponent<Transform>().position = shootingPoint.position; 
        
        float angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);

        newProjectile.GetComponent<Projectile>().Shoot(new ProjectileParameters(shootingPoint.position, direction, Damage, Team));
    }

    protected override void DisposeAttack()
    {
        //throw new System.NotImplementedException();
    }
}
