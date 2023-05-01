using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDealingDamageByTouch : ProjectileDealingDamage
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tag.getTag(Team))
            return;

        //deal damage
    }
}
