using UnityEngine;

namespace Game.Enemy.Fitness
{
    public class Example : FitnessFunction
    {
        protected override float CalculateFitnessFuction()
        {
            float distance = Enemy.EnemyMovement.GetDistanceFromPlayer();
            return 1 / (distance + 1);
        }
    }
}