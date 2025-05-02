using Train.Player;
using UnityEngine;

namespace Train.Arena
{
    public class ArenaController : MonoBehaviour
    {
        [field: SerializeField]
        public GameBoardCreater Board { get; private set; }
        [field: SerializeField]
        public EnemySpawner EnemySpawner { get; private set; }
        [field: SerializeField]
        public PlayerSpawner PlayerSpawner { get; private set; }

        private int _id;

        public Vector2 Position { get; private set; }
        public void Initialize(int id, Vector2 position)
        {
            _id = id;
            Position = position;
            Board.SummonBoard(position);
            PlayerSpawner.SpawnPlayer();
            EnemySpawner.SpawnEnemies();
        }

        public void RecreateArena()
        {
            EnemySpawner.RecreateEnemies();
            PlayerSpawner.RecreatePlayer();
        }
    }
}