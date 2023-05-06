using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponVisitor
{
    public void Visit(ProjectileDealingDamageByTouch deal, Vector2 direction);
}
