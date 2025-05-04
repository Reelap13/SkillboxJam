using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawnerAdapter : PlayerSpawner
    {
        [SerializeField] private PlayerController _player_controller;

        private void Awake()
        {
            _block_stats_calculation = true;
        }

        public override Transform GetPlayer()
        {
            return _player_controller.transform;
        }
        public override Vector2 GetPlayerPosition()
        {
            return _player_controller.transform.position;
        }

        public override Coordinates GetPlayerPredictedInput()
        {
            return new(new(0, 0));
        }

        public override PlayerWeaponType GetPlayerWeaponType()
        {
            return PlayerWeaponType.W1;
        }
    }
}