using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttackGiveDamage : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnGiveDamage = new UnityEvent();
    [field: SerializeField]
    public MeleeAttack MeleeAttack { get; private set; }

    public float Damage => MeleeAttack.MeleeAttackParameters.Damage;
    public TagEnum Team => MeleeAttack.MeleeAttackPreset.Team;
    public Push2D Push => MeleeAttack.Push;

    private LinkedList<Collider2D> aims = new LinkedList<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Tag.IsParticipantOfTeam(collision.tag) && collision.tag != Tag.getTag(Team))
            aims.AddLast(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Tag.IsParticipantOfTeam(collision.tag) && collision.tag != Tag.getTag(Team))
            aims.Remove(collision);
    }

    public void GiveDamage()
    {
        foreach (Collider2D collider in aims)
            collider.GetComponent<IWeaponVisitor>()?.Visit(this);
        OnGiveDamage.Invoke();
        Destroy(gameObject);
    }

}
