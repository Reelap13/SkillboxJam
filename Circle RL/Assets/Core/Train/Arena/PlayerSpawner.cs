using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] protected ArenaController _controller;
        [SerializeField] private PlayerAI _player_prefab;

        protected PlayerAI _player;
        protected PlayerStats _player_stats;

        protected bool _block_stats_calculation = false;

        private void Update()
        {
            if (_player_stats == null || _block_stats_calculation)
                return;
            _player_stats.UpdatePlayerData();
        }

        public void SpawnPlayer()
        {
            _player = Instantiate(_player_prefab);
            _player.transform.parent = transform;
            _player.transform.position = _controller.Board.GetRandomPoint();
            _player.Health.OnDie.AddListener(ProcessPlayerDie);

            _player_stats = new();
            _player_stats.InitializePlayer(_player);

            _player.Initialize(_controller);
        }

        public void RecreatePlayer()
        {
            PlayerAI player = _player;
            ProcessPlayerDie();
            Destroy(player.gameObject);
        }

        private void ProcessPlayerDie()
        { 
            _player.Health.OnDie.RemoveListener(ProcessPlayerDie);
            _player = null;
            _player_stats = null;
            SpawnPlayer();
        }

        public PlayerData GetPlayerData()
        {
            return _player.DataTaker.GetData();
        }

        public void ProcessCommand(PlayerCommand command)
        {
            _player.Behaviour.ProcessCommand(command);
        }

        public Transform GetPlayer()
        {
            return _player.transform;
        }
        public Vector2 GetPlayerPosition()
        {
            return _player.transform.position;
        }

        public Coordinates GetPlayerPredictedInput()
        {
            return new (new(0, 0));
        }

        public PlayerWeaponType GetPlayerWeaponType()
        {
            return PlayerWeaponType.W1;
        }

        public PlayerStats Stats { get { return _player_stats; } private set { } }
    }
}