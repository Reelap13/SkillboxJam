using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDealingDamageByTouch : ProjectileDealingDamage
{
    private bool isDamage = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamage)
            return;

        if (collision.tag == Tag.getTag(Team))
            return;

        if (!Tag.IsParticipantOfTeam(collision.tag))
            return;

        isDamage = true;
        IWeaponVisitor weaponVisitor = collision.GetComponent<IWeaponVisitor>();
        weaponVisitor?.Visit(this);
        Destroy(gameObject);
    }
}
