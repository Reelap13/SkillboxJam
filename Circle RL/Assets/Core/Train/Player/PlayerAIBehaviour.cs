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
            Player.Movement.SetDirection(command.Direction);
            if (command.IsAttack)
                Player.Weapon.PerformAttack();
        }
    }
}