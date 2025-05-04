using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawnerAdapter : PlayerSpawner
    {
        [SerializeField] private PlayerAI _player_ai;

        private void Awake()
        {
            _player = _player_ai;
            _block_stats_calculation = true;
        }
    }
}