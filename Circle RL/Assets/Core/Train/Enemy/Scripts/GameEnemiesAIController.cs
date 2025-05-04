using UnityEngine;

namespace Game.Enemy
{
    public class GameEnemiesAIController : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerAdapter _enemies;
        [SerializeField] private NeurallNetworks _nns;
        [SerializeField] private PlayerController _player;
        [SerializeField] private float _comand_update_time = 0.2f;

        private float _time;

        private void Awake()
        {
            EnemyAIDataTaker.BLOCK_FITNESS_CALCULATION = true;
        }

        private void Update()
        {
            _time += Time.deltaTime;   
            if (_time >= _comand_update_time && _player != null)
            {
                _time = 0f;
                UpdateEnemiesCommand();
            }
        }

        private void UpdateEnemiesCommand()
        {
            foreach (var enemy in _enemies.Enemies)
                enemy.EnemyBehavior.ProcessCommnad(_nns.ProcessEnemyData(enemy.GetData()));
        }
    }
}