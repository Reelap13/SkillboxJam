using UnityEngine;

namespace Game.Enemy.Fitness
{
    public class BomberFitnessFunction : EnemyFitnessFunction
    {
        protected override float CalculateFitnessFuction()
        {
            float punishment = Stats.GetTotalHPLost() / 10 + Stats.TotalDeaths() * 2;
            float reward = 100 / (Mathf.Pow(Mathf.Abs(Stats.GetAvarageDistanceToTarget()), 2) + 0.01f) + Mathf.Pow(Stats.GetTotalAttackNumber(), 2);

            return reward - punishment;
        }
    }
}