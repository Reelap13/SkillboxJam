using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyStats
    {
        private AIEnemy _enemy;

        private float _damage;
        private float _hp_lost;
        private List<float> _distance_to_target = new();
        private int _kills;
        private int _deaths;
        private int _attack_number;

        public void UpdateEnemyData()
        {
            _distance_to_target.Add(_enemy.EnemyMovement.GetDistanceFromPlayer());
        }

        public void InitializeEnemy(AIEnemy enemy)
        {
            _enemy = enemy;
            _enemy.EnemyAbility.OnKillingPlayer.AddListener(() => { _kills += 1; });
            _enemy.EnemyAbility.OnMakingDamage.AddListener((float damage) => { _damage += damage; });

            _enemy.EnemyParameters.OnDieing.AddListener(() => { _deaths += 1; });
            _enemy.EnemyParameters.OnLosedHealth.AddListener((float damage) => { _hp_lost += damage; });
            _enemy.EnemyBehavior.OnPerformedAbility.AddListener(() => { _attack_number += 1; });
        }

        public float GetTotalDamage()
        {
            return _damage;
        }

        public float GetTotalHPLost()
        {
            return _hp_lost;
        }

        public float GetAvarageDistanceToTarget()
        {
            float avarage_distance = 0f;
            foreach (var distance in _distance_to_target)
                avarage_distance += distance;
            return avarage_distance / _distance_to_target.Count;
        }

        public int TotalKills()
        {
            return _kills;
        }

        public int TotalDeaths()
        {
            return _deaths;
        }

        public int GetTotalAttackNumber()
        {
            return _attack_number;
        }
    }
}