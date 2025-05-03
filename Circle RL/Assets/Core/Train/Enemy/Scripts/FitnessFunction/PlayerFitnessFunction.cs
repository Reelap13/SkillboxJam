using Train.Player;
using UnityEngine;


namespace Game.Enemy.Fitness
{
    public class PlayerFitnessFunction : FitnessFunction
    {
        [field: SerializeField]
        public PlayerAI Player { get; private set; }

        public PlayerStats Stats => Player.ArenaController.PlayerSpawner.Stats;

        protected override float CalculateFitnessFuction()
        {
            float punishment = Mathf.Pow(Stats.GetTotalHPLost(), 2)/10 + Mathf.Pow(Stats.TotalDeaths(), 2) * 100;
            float reward = Mathf.Pow(Stats.GetAvarageDistanceToTarget(), 2) * 10;

            return reward - punishment;
        }
    }
}