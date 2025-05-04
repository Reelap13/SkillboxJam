using System;
using System.Collections.Generic;
using Game.Enemy;
using Train.AIConnection.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Train.Arena
{
    public class EnemySpawner : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnAllEnemiesDie = new();

        [SerializeField] protected ArenaController _controller;
        [SerializeField] private List<AIEnemy> _enemies_prefab;

        protected HashSet<AIEnemy> _alive_enemies = new();
        protected Dictionary<EnemyType, EnemyStats> _enemy_stats = new();

        protected bool _block_stats_calculation = false;

        private void Update()
        {
            if (_alive_enemies.Count != _enemies_prefab.Count || _block_stats_calculation)
                return;
            foreach (var enemy in _alive_enemies)
            {
                if (enemy.gameObject == null) continue;
                GetEnemyStats(enemy).UpdateEnemyData();
            }
        }

        public void SpawnEnemies()
        {
            _alive_enemies.Clear();
            _enemy_stats.Clear();
            foreach (var enemy_prefab in _enemies_prefab)
            {
                AIEnemy enemy = SpawnEnemy(enemy_prefab);
                _alive_enemies.Add(enemy);
                enemy.OnDie.AddListener(OnEnemyDie);

                EnemyStats stats = new();
                _enemy_stats.Add(enemy.EnemyPreset.Type, stats);

                stats.InitializeEnemy(enemy);
                enemy.Initialize(_controller);
            }
        }
        
        public void RecreateEnemies()
        {
            foreach (var enemy in _alive_enemies)
                Destroy(enemy.gameObject);
            SpawnEnemies();
        }

        private void OnEnemyDie(AIEnemy enemy)
        {
            enemy.OnDie.RemoveListener(OnEnemyDie);
            _alive_enemies.Remove(enemy);

            foreach (var enemy_prefab in _enemies_prefab)
                if (enemy_prefab.EnemyPreset.Type == enemy.EnemyPreset.Type)
                {
                    AIEnemy ai_enemy = SpawnEnemy(enemy_prefab);
                    _alive_enemies.Add(ai_enemy);
                    ai_enemy.OnDie.AddListener(OnEnemyDie);
                    ai_enemy.Initialize(_controller);
                    break;
                }
        }

        private AIEnemy SpawnEnemy(AIEnemy enemy_prefab)
        {
            AIEnemy enemy = Instantiate(enemy_prefab);
            enemy.transform.parent = transform;
            enemy.transform.position = _controller.Board.GetRandomPoint();

            return enemy;
        }

        public Transform GetClosestEnemyToPoint(Vector2 position)
        {
            float min_distance = float.MaxValue;
            Transform closest_enemy = null;
            foreach (var enemy in _alive_enemies)
            {
                float distance = Vector2.Distance(position, enemy.transform.position);
                if (distance < min_distance)
                {
                    min_distance = distance;
                    closest_enemy = enemy.transform;
                }
            }

            return closest_enemy;
        }

        public List<EnemyData> GetEnemyData()
        {
            List<EnemyData> data = new List<EnemyData>();
            foreach (var enemy in _alive_enemies)
                data.Add(enemy.GetData());

            return data;
        }

        public void ProcessCommand(EnemyCommand command)
        {
            foreach (var enemy in _alive_enemies)
                if (enemy.EnemyPreset.Type == command.Type)
                {
                    enemy.EnemyBehavior.ProcessCommnad(command);
                    break;
                }
        }

        public EnemyStats GetEnemyStats(AIEnemy enemy)
        {
            return _enemy_stats[enemy.EnemyPreset.Type];
        }
    }
}