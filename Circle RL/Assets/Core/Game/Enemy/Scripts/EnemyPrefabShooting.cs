using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabShooting : EnemyAbility
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private int countOfShooting = 1;
    [SerializeField, Range(0, 90f)] private float scatter = 0;

    private GameObject projectilePref;
    private Transform tr;
    
    private void Awake()
    {
        projectilePref = projectile.gameObject;
        tr = GetComponent<Transform>();
    }

    public override void PerformAbility(Vector2 direction)
    {
        for (int i = 0; i < countOfShooting; ++i)
        {
            Shoot(direction);
        }
    }

    private void Shoot(Vector2 direction)
    {
        float scatterAngle = Random.Range(-scatter, scatter) * Mathf.PI / 180;
        direction = (new Vector3(direction.x * Mathf.Cos(scatterAngle) - direction.y * Mathf.Sin(scatterAngle),
            direction.x * Mathf.Sin(scatterAngle) + direction.y * Mathf.Cos(scatterAngle),
            0)).normalized;

        GameObject newProjectile = Instantiate(projectilePref) as GameObject;
        newProjectile.GetComponent<Transform>().position = tr.position;

        float angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);

        newProjectile.GetComponent<Projectile>().Shoot(new ProjectileParameters(tr.position, direction, Damage, enemy.EnemyPreset.Team, 1, 0));
    }
}
