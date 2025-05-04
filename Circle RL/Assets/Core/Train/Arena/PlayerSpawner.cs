using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] protected ArenaController _controller;
        [SerializeField] private PlayerAI _player_prefab;

        private PlayerAI _player;
        private PlayerStats _player_stats;
        protected InputPredictor _input_predictor;

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
            _input_predictor = _player.GetComponent<InputPredictor>();

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

        public virtual Transform GetPlayer()
        {
            return _player.transform;
        }
        public virtual Vector2 GetPlayerPosition()
        {
            return _player.transform.position;
        }

        public virtual Coordinates GetPlayerPredictedInput()
        {
            Vector2 p = _input_predictor.GetPredictedDirection();
            Debug.Log(p);
            return new Coordinates(p);
        }

        public virtual PlayerWeaponType GetPlayerWeaponType()
        {
            return  _player.Weapon.Type;
        }

        public PlayerStats Stats { get { return _player_stats; } private set { } }
    }
}