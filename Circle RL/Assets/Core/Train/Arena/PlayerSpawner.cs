using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private ArenaController _controller;
        [SerializeField] private PlayerAI _player_prefab;

        private PlayerAI _player;
        private PlayerStats _player_stats;

        private void Update()
        {
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

        public PlayerInputType GetPlayerPredictedInput()
        {
            return PlayerInputType.W;
        }

        public PlayerWeaponType GetPlayerWeaponType()
        {
            return PlayerWeaponType.W1;
        }
    }
}