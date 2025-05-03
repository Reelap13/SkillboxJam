using Train.Player;
using UnityEngine;


namespace Game.Enemy.Fitness
{
    public class ExamplePlayer : FitnessFunction
    {
        [field: SerializeField]
        public PlayerAI Player { get; private set; }

        public PlayerStats Stats => Player.ArenaController.PlayerSpawner.Stats;

        protected override float CalculateFitnessFuction()
        {
            return 0f;
        }
    }
}