using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private ArenaController _controller;
        [SerializeField] private GameObject _player_prefab;

        private GameObject _player;

        public void SpawnPlayer()
        {
            _player = Instantiate(_player_prefab);
            _player.transform.parent = transform;
            _player.transform.position = _controller.Board.GetMiddleOfBoard();
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