using System;
using System.Collections;
using Train.AIConnection.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class AIEnemyBehaviour : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnPerformedAbility = new();

        [field: SerializeField] 
        public AIEnemy Enemy { get; private set; }

        [SerializeField] private Vector2 _interval_to_attack = new(0, 100);
        [SerializeField] private float _ability_cooldown = 0.5f;

        private bool _is_ready_to_perfaorm_ability = true;

        public void ProcessCommnad(EnemyCommand command)
        {
            Enemy.EnemyMovement.SetDirection(command.Direction);
            if (command.IsAttack && IsInAttackInterval() && _is_ready_to_perfaorm_ability)
            {
                StartCoroutine(Deley());
                Enemy.EnemyAbility.PerformAbility(Enemy.EnemyMovement.GetDirectionToPlayer());
                OnPerformedAbility.Invoke();
            }
        }

        private bool IsInAttackInterval()
        {
            float distance_from_player = Enemy.EnemyMovement.GetDistanceFromPlayer();
            return distance_from_player >= _interval_to_attack.x && distance_from_player <= _interval_to_attack.y;  
        }

        private IEnumerator Deley()
        {
            _is_ready_to_perfaorm_ability = false;
            yield return new WaitForSeconds(_ability_cooldown);
            _is_ready_to_perfaorm_ability = true;
        }
    }
}