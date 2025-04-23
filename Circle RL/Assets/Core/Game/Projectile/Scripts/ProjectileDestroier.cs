using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroier : MonoBehaviour
{
    [SerializeField] private ProjectileTypes[] deletionTypes;
    private string projectileTag = Tag.getTag(TagEnum.PROJECTILE);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != projectileTag)
            return;

        Projectile projectile = collision.GetComponent<Projectile>();
        foreach (ProjectileTypes types in deletionTypes)
            if (types == projectile.ProjectilePreset.ProjectileTypes)
                Destroy(collision.gameObject);
    }
}
