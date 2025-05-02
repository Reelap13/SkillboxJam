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

        public void SpawnPlayer()
        {
            _player = Instantiate(_player_prefab);
            _player.transform.parent = transform;
            _player.transform.position = _controller.Board.GetRandomPoint();
            _player.Health.OnDie.AddListener(ProcessPlayerDie);
            _player.Initialize(_controller);
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