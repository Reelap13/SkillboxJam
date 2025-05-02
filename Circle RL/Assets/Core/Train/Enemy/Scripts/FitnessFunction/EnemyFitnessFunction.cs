using UnityEngine;

namespace Game.Enemy.Fitness
{
    public abstract class EnemyFitnessFunction : FitnessFunction
    {
        [field: SerializeField]
        public AIEnemy Enemy { get; private set; }
    }
}