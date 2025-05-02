using Game.Enemy.Fitness;
using Train.AIConnection.Data;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyAIDataTaker : MonoBehaviour
    {
        [SerializeField] private AIEnemy _enemy;
        [SerializeField] private SensorsTaker _sensors_taker;
        [SerializeField] private FitnessFunction _fitness_function;

        public EnemyData GetData()
        {
            EnemyData data = new EnemyData();

            data.EnemyType = _enemy.EnemyPreset.Type;
            data.Sensors = _sensors_taker.GetSensorsData();
            data.Position = _enemy.ArenaController.Board.GetParsedPosition(transform.position);
            data.TargetPosition = _enemy.ArenaController.Board.GetParsedPosition(_enemy.ArenaController.PlayerSpawner.GetPlayerPosition());
            data.HP = _enemy.EnemyParameters.Health;
            data.PlayerInputPredict = _enemy.ArenaController.PlayerSpawner.GetPlayerPredictedInput();
            data.PlayerWeaponType = _enemy.ArenaController.PlayerSpawner.GetPlayerWeaponType();

            _fitness_function.CalculateFitness();
            data.MaxScore = _fitness_function.MaxFitness;
            data.CurrentScore = _fitness_function.CurrentFitness;

            return data;
        }
    }
}