using Game.Enemy;
using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerAIBehaviour : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerAI Player { get; private set; }

        public void ProcessCommand(PlayerCommand command)
        {
            Player.Movement.SetDirection(ParseDirection(command));
        }

        private Vector2 ParseDirection(PlayerCommand command)
        {
            Vector2 direction = Vector2.zero;
            if (command.W) direction += Vector2.up;
            if (command.A) direction += Vector2.left;
            if (command.S) direction += Vector2.down;
            if (command.D) direction += Vector2.right;

            return direction.normalized;
        }
    }
}