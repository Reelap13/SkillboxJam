using UnityEngine;


namespace Game.Enemy
{
    public class EnemyAITakerDamage : MonoBehaviour, IWeaponVisitor
    {
        [SerializeField] private AIEnemy enemy;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Visit(ProjectileDealingDamageByTouch deal, Vector2 direction)
        {
            rb?.AddForce(direction * deal.PushForce);
            enemy.EnemyParameters.TakeDamage(deal.Damage);
        }

        public void Visit(MeleeAttackGiveDamage deal)
        {
            //throw new System.NotImplementedException();
        }
    }
}