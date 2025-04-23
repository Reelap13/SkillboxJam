using Train.AIConnection.Data;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyAIDataTaker : MonoBehaviour
    {
        [SerializeField] private AIEnemy _enemy;
        [SerializeField] private SensorsTaker _sensors_taker;

        public EnemyData GetData()
        {
            EnemyData data = new EnemyData();

            data.EnemyType = _enemy.EnemyPreset.Type;
            data.Sensors = _sensors_taker.GetSensorsData();
            data.EnemyCoordinates = _enemy.ArenaController.Board.GetParsedPosition(transform.position);
            data.PlayerCoordinates = _enemy.ArenaController.Board.GetParsedPosition(_enemy.ArenaController.PlayerSpawner.GetPlayerPosition());
            data.HP = _enemy.EnemyParameters.Health;
            data.PlayerInputPredict = _enemy.ArenaController.PlayerSpawner.GetPlayerPredictedInput();
            data.PlayerWeaponType = _enemy.ArenaController.PlayerSpawner.GetPlayerWeaponType();

            return data;
        }
    }
}