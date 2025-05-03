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

        [SerializeField] private ArenaController _controller;
        [SerializeField] private List<AIEnemy> _enemies_prefab;

        private HashSet<AIEnemy> _alive_enemies = new();
        private Dictionary<EnemyType, EnemyStats> _enemy_stats = new();

        private void Update()
        {
            foreach (var enemy in _alive_enemies)
            {
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
                enemy.Initialize(_controller);

                EnemyStats stats = new();
                stats.InitializeEnemy(enemy);
                _enemy_stats.Add(enemy.EnemyPreset.Type, stats);
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
            Debug.Log($"{_alive_enemies.Count} {_enemy_stats.Count}");
            return _enemy_stats[enemy.EnemyPreset.Type];
        }
    }
}