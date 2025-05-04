using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawnerAdapter : PlayerSpawner
    {
        [SerializeField] private PlayerController _player_controller;

        private void Awake()
        {
            _input_predictor = _player_controller.GetComponent<InputPredictor>();
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
            Vector2 p = _input_predictor.GetPredictedDirection();
            Debug.Log(p);
            return new(p);
        }

        public override PlayerWeaponType GetPlayerWeaponType()
        {
            return PlayerWeaponType.W1;
        }
    }
}