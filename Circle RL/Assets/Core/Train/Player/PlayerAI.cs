using Train.Arena;
using UnityEngine;

namespace Train.Player
{
    public class PlayerAI : MonoBehaviour
    {

        [field: SerializeField]
        public PlayerAIBehaviour Behaviour { get; private set; }
        [field: SerializeField]
        public PlayerAIMovement Movement { get; private set; }
        [field: SerializeField]
        public PlayerAIWeapon Weapon { get; private set; }
        [field: SerializeField]
        public PlayerAIDataTaker DataTaker { get; private set; }
        [field: SerializeField]
        public PlayerHealth Health { get; private set; }

        public ArenaController ArenaController { get; private set; }

        public void Initialize(ArenaController _controller)
        {
            ArenaController = _controller;
        }
    }
}