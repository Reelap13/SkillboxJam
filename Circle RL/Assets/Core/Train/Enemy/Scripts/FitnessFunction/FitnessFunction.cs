using UnityEngine;

namespace Game.Enemy.Fitness
{
    public abstract class FitnessFunction : MonoBehaviour
    {
        [field: SerializeField]
        public AIEnemy Enemy { get; private set; }

        private float _max_fitness = 0f;
        private float _current_fitness = 0f;

        public void CalculateFitness()
        {
            _current_fitness = CalculateFitnessFuction();
            if (_current_fitness > _max_fitness)
                _max_fitness = _current_fitness;
        }

        protected abstract float CalculateFitnessFuction();

        public float MaxFitness { get { return _max_fitness; } }
        public float CurrentFitness { get { return _current_fitness; } }
    }
}