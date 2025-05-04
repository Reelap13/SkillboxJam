using UnityEngine;

namespace Game.Enemy.Fitness
{
    public class SpawnerFitnessFunction : EnemyFitnessFunction
    {
        protected override float CalculateFitnessFuction()
        {
            float punishment = Mathf.Pow(Stats.GetTotalHPLost(), 2) / 10 + Mathf.Pow(Stats.TotalDeaths(), 2) * 100;
            float reward = (Mathf.Pow(Stats.GetAvarageDistanceToTarget(), 2) + 0.01f) + Mathf.Pow(Stats.GetTotalAttackNumber(), 2);

            return reward - punishment;
        }
    }
}