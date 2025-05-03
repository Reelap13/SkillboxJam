using Game.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Train.Player
{
    public class PlayerStats
    {
        private PlayerAI _player;

        private float _damage;
        private float _hp_lost;
        private List<float> _distance_to_target;
        private int _kills;
        private int _deaths;

        public void UpdatePlayerData()
        {
            _distance_to_target.Add(_player.Movement.GetDistanceToClosestEnemy());
        }

        public void InitializePlayer(PlayerAI player)
        {
            _player = player;

            _player.Health.OnDie.AddListener(() => { _deaths += 1; });
            _player.Health.loseHitPoint.AddListener((float damage) => { _hp_lost += damage; });
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
    }
}