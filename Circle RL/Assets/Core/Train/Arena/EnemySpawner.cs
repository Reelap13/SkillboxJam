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

        public void SpawnEnemies()
        {
            foreach (var enemy_prefab in _enemies_prefab)
            {
                AIEnemy enemy = SpawnEnemy(enemy_prefab);
                _alive_enemies.Add(enemy);
                enemy.OnDie.AddListener(OnEnemyDie);
                enemy.Initialize(_controller);
            }
        }

        private void OnEnemyDie(AIEnemy enemy)
        {
            enemy.OnDie.RemoveListener(OnEnemyDie);
            _alive_enemies.Remove(enemy);
            if (_alive_enemies.Count == 0)
                OnAllEnemiesDie.Invoke();
        }

        private AIEnemy SpawnEnemy(AIEnemy enemy_prefab)
        {
            AIEnemy enemy = Instantiate(enemy_prefab);
            enemy.transform.parent = transform;
            enemy.transform.position = _controller.Board.GetRandomPoint();

            return enemy;
        }

        public List<EnemyData> GetEnemyData()
        {
            List<EnemyData> data = new List<EnemyData>();
            foreach (var enemy in _alive_enemies)
                data.Add(enemy.GetData());

            return data;
        }
    }
}