using UnityEngine;

namespace Game.Enemy.Fitness
{
    public class SolderFitnessFunction : EnemyFitnessFunction
    {
        protected override float CalculateFitnessFuction()
        {
            float punishment = Mathf.Pow(Stats.GetTotalHPLost(), 2) / 10 + Mathf.Pow(Stats.TotalDeaths(), 2) * 100;
            float reward = 10 / (Mathf.Pow(Mathf.Abs(8 - Stats.GetAvarageDistanceToTarget()), 2) + 0.01f);

            return reward - punishment;
        }
    }
}