using UnityEngine;

namespace Game.Enemy.Fitness
{
    public class MeleeFighterFitnessFunction : EnemyFitnessFunction
    {
        protected override float CalculateFitnessFuction()
        {
            float punishment = Mathf.Pow(Stats.GetTotalHPLost(), 2) / 30 + Mathf.Pow(Stats.TotalDeaths(), 2) * 40;
            float reward = 10 / (Mathf.Pow(Mathf.Abs(1 - Stats.GetAvarageDistanceToTarget()), 2) + 0.01f);

            return reward - punishment;
        }
    }
}