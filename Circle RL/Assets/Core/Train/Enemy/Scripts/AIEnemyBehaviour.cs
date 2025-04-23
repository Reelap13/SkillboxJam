using Train.AIConnection.Data;
using UnityEngine;

namespace Game.Enemy
{
    public class AIEnemyBehaviour : MonoBehaviour
    {
        [field: SerializeField] 
        public AIEnemy Enemy { get; private set; }

        [SerializeField] private Vector2 _interval_to_attack = new(0, 100);

        public void ProcessCommnad(EnemyCommand command)
        {
            Enemy.EnemyMovement.SetDirection(command.Direction);
            if (command.IsAttack && IsInAttackInterval())
                Enemy.EnemyAbility.PerformAbility(Enemy.EnemyMovement.GetDirectionToPlayer());
        }

        private bool IsInAttackInterval()
        {
            float distance_from_player = Enemy.EnemyMovement.GetDistanceFromPlayer();
            return distance_from_player >= _interval_to_attack.x && distance_from_player <= _interval_to_attack.y;  
        }
    }
}