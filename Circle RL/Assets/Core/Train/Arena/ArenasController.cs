using System.Collections;
using System.Collections.Generic;
using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train
{
    public class ArenasController : MonoBehaviour
    {
        [SerializeField] private ArenaController _arena_prefab;
        [SerializeField] private int _arena_area_size = 3;
        [SerializeField] private float _arenas_offset = 20f;

        private List<ArenaController> _arenas;

        private void Awake()
        {
            StartCoroutine(test());
        }

        private IEnumerator test()
        {
            yield return new WaitForSeconds(2);
            GetEnemiesData();
        }

        public void Initialize()
        {
            _arenas = new List<ArenaController>();
            for (int i = 0; i < _arena_area_size; ++i)
            {
                for (int j = 0; j < _arena_area_size; ++j)
                {
                    ArenaController arena = Instantiate(_arena_prefab);
                    arena.transform.parent = transform;
                    arena.Initialize(i * _arena_area_size + j, new Vector2(i, j) * _arenas_offset);
                    _arenas.Add(arena);
                }
            }
        }
        
        public string GetEnemiesData()
        {
            List<EnemyData> solders_data = new List<EnemyData>();
            List<EnemyData> snipers_data = new List<EnemyData>();
            foreach (var arena in _arenas)
                foreach (var enemy_data in arena.EnemySpawner.GetEnemyData())
                    switch (enemy_data.EnemyType)
                    {
                        case Game.Enemy.EnemyType.SOLDER: 
                            solders_data.Add(enemy_data);
                            break;
                        case Game.Enemy.EnemyType.SNIPER: 
                            solders_data.Add(enemy_data);
                            break;
                    }

            EnemiesData data = new EnemiesData();
            data.Solders = solders_data.ToArray();
            data.Snipers = snipers_data.ToArray();

            Debug.Log(JsonUtility.ToJson(data));
            return JsonUtility.ToJson(data);
        }

        public void ProcessEnemyCommand(EnemiesCommands commands)
        {
            for (int i = 0; i < _arenas.Count; ++i)
            {
                EnemySpawner enemies = _arenas[i].EnemySpawner;
                enemies.ProcessCommand(commands.Solders[i]);
                //enemies.ProcessCommand(commands.Snipers[i]);
            }
            
        }
    }
}