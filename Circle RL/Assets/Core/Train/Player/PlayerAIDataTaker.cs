using Game.Enemy;
using Game.Enemy.Fitness;
using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerAIDataTaker : MonoBehaviour
    {
        [SerializeField] private PlayerAI _player;
        [SerializeField] private SensorsTaker _sensors_taker;
        [SerializeField] private FitnessFunction _fitness_function;

        public PlayerData GetData()
        {
            PlayerData data = new PlayerData();

            data.Sensors = _sensors_taker.GetSensorsData();
            data.PlayerCoordinates = _player.ArenaController.Board.GetParsedPosition(_player.transform.position);
            data.ClosestEnemyCoordinates = _player.ArenaController.Board.GetParsedPosition(
                _player.ArenaController.EnemySpawner.GetClosestEnemyToPoint(_player.transform.position).position);
            data.HP = _player.Health.HitPoint;
            data.WeaponType = _player.Weapon.Type;
            data.MaxScore = _fitness_function.MaxFitness;
            data.CurrentScore = _fitness_function.CurrentFitness;

            return data;
        }
    }
}