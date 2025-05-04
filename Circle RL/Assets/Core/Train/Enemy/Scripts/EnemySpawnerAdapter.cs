using System.Collections.Generic;
using NUnit.Framework;
using Train.Arena;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySpawnerAdapter : EnemySpawner
    {

        private void Awake()
        {
            _alive_enemies.Clear();
            _block_stats_calculation = true;
            AIEnemy.OnSpawned.AddListener(OnSpawnedEnemy);
            AIEnemy.OnDied.AddListener(OnDiedEnemy);
        }

        private void OnSpawnedEnemy(AIEnemy enemy)
        {
            _alive_enemies.Add(enemy);
            enemy.Initialize(_controller);
        }
        private void OnDiedEnemy(AIEnemy enemy)
        {
            _alive_enemies.Remove(enemy);
        }

        public HashSet<AIEnemy> Enemies => _alive_enemies;
    }
}